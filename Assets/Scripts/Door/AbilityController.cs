using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JetBrains.Annotations;
using UnityEditor.Overlays;
using UnityEditor.Playables;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AbilityController : Singleton<AbilityController>
{
    public List<IAbility> availableAbilitiesForRound = new(); // This holds the abilities for this round
    public List<IAbility> typesOfAbilitesUnlocked = new(); // This holds the total abilities the player unlocked in the run
    public IAbility activeAbility; // Utilitary, if needed
    public AbilityListSO abilitiesSo; // Total abilities in the game. Distinct.

    public GameObject HellPortalPrefab;
    public GameObject VacuumPrefab;
    public GameObject TentaclePrefab;
    public GameObject LaserPrefab;
    public GameObject KeyPrefab;

    public Action<IAbility> onAbilityUsed;
    public Action<List<IAbility>> onAbilitiesLoaded;
    public Dictionary<int, bool> doorsInUse;

    public void Start()
    {
        HellPortalPrefab = Resources.Load<GameObject>("Prefabs/HellPortal");
        VascuumPrefab = Resources.Load<GameObject>("Prefabs/Vacuum");
        LaserPrefab = Resources.Load<GameObject>("Prefabs/Laser");
        abilitiesSo = Resources.Load<AbilityListSO>("ScriptableObjects/AbilityList");
        KeyPrefab = Resources.Load<GameObject>("Prefabs/KeySprite");
        DoorEventManager.ActivateDoor += OnDoorActivated;
        UpdateAbilitiesForRound();
    }

    public void Update()
    {

    }
    #region Activate abilities
    public void ActivateNextAbility(GameObject door)
    {
        var nextAbility = availableAbilitiesForRound.First();
        Debug.Log(nextAbility.GetAbilitySo().Name);
        var abilityObj = Instantiate(nextAbility.GetAbilitySo().AbilityPrefab, door.transform.position, Quaternion.identity);
        abilityObj.GetComponent<IAbility>().Activate(door);
        activeAbility = nextAbility;
        onAbilityUsed?.Invoke(activeAbility);
    }

    #endregion

    /// <summary>
    /// Add ability should be used at the end of round screen where you receive a key
    /// </summary>
    /// <param name="ability"></param>
    public void UnlockAbility(IAbility ability)
    {
        typesOfAbilitesUnlocked.Add(ability);
    }


    /// <summary>
    /// Add ability should be used at the end of round screen where you receive a key
    /// </summary>
    /// <param name="ability"></param>
    public void UnlockAbility(AbilitySO ability)
    {
        AddAbilityToListFromName(ability.Name, typesOfAbilitesUnlocked);
    }

    public void UnlockAbility(string abName)
    {
        AddAbilityToListFromName(abName, typesOfAbilitesUnlocked);
    }

    public void AddAbilityForRound(AbilitySO ability)
    {
        AddAbilityToListFromName(ability.Name, availableAbilitiesForRound);
        Debug.Log($"Invoking {availableAbilitiesForRound.Count}");
        onAbilitiesLoaded?.Invoke(availableAbilitiesForRound);
    }

    public void AddAbilityForRound(IAbility ability)
    {
        availableAbilitiesForRound.Add(ability);
        Debug.Log($"Invoking {availableAbilitiesForRound.Count}");
        onAbilitiesLoaded?.Invoke(availableAbilitiesForRound);
    }

    public void AddAbilityForRound(string abName)
    {
        AddAbilityToListFromName(abName, availableAbilitiesForRound);
        Debug.Log($"Invoking {availableAbilitiesForRound.Count}");
        onAbilitiesLoaded?.Invoke(availableAbilitiesForRound);
    }
    [ContextMenu("Add hell portal")]
    public void AddPortal()
    {
        AddAbilityForRound("Hell Portal");

    }
    [ContextMenu("Add vacuum")]
    public void AddVacuum()
    {
        AddAbilityForRound("Vacuum");
    }
    [ContextMenu("Add laser")]
    public void AddLaser()
    {
        AddAbilityForRound("Laser");
    }
    [ContextMenu("Add tentacle")]
    public void AddTentacle() {
        AddAbilityForRound("Tentacle");
    }


    /// <summary>
    /// Consumes the current used ability so it can't be used again.
    /// </summary>
    /// <param name="ability"></param>
    public void RemoveAbility(IAbility ability)
    {
        availableAbilitiesForRound.Remove(ability);
    }

    public void RemoveAbility(int index)
    {
        availableAbilitiesForRound.RemoveAt(index);
    }

    /// <summary>
    /// This should be called when the round changes, to refresh the active keys for the door.
    /// </summary>
    public void UpdateAbilitiesForRound()
    {
        availableAbilitiesForRound.Clear();
    }

    [ContextMenu("UNLOCK ALL ABILITIES")]
    public void UnlockAllAbilities()
    {
        typesOfAbilitesUnlocked.Clear();
        abilitiesSo.Abilities.ForEach(ability => AddAbilityToListFromName(ability.name, typesOfAbilitesUnlocked));
    }

    public void UseAbility(GameObject door)
    {
        if (availableAbilitiesForRound.Any())
        {
            Debug.Log("Activate");
            ActivateNextAbility(door);
            availableAbilitiesForRound.RemoveAt(0);
        }
    }

    void AddAbilityToListFromName(string abilityName, List<IAbility> abilities)
    {
        switch (abilityName)
        {
            case "Hell Portal":
                abilities.Add(HellPortalPrefab.GetComponent<HellPortal>());
                break;
            case "Vacuum":
                abilities.Add(VacuumPrefab.GetComponent<Vacuum>());
                break;
            case "Tentacle":
                abilities.Add(TentaclePrefab.GetComponent<TentacleAbility>());
                break;
            case "Laser":
                abilities.Add(LaserPrefab.GetComponent<Laser>());
                break;
            default:
                break;
        }
    }

    void OnDoorActivated(GameObject door)
    {
        var nextAbility = GetNextAbility();
        if (nextAbility != null)
        {
            door.GetComponent<DoorEventManager>().isUsingAbility = true; 
            var spawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.up * 2; // Adjust as needed
            var keyObject = Instantiate(KeyPrefab, spawnPosition, Quaternion.identity);
            keyObject.SetActive(true);
            keyObject.GetComponent<SpriteRenderer>().sprite = nextAbility.GetAbilitySo().KeyIcon;
            keyObject.GetComponent<SpriteRenderer>().color = nextAbility.GetAbilitySo().KeyColor;
            StartCoroutine(MoveSpriteToDoor(keyObject, door.transform.position, door, 2f));

        }
    }
    IEnumerator MoveSpriteToDoor(GameObject sprite, Vector3 targetPosition, GameObject door, float duration)
    {
        Debug.Log("Moving");
        float time = 0;
        Vector3 startPosition = sprite.transform.position;

        while (time < duration)
        {
            sprite.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        sprite.transform.position = targetPosition;

        Destroy(sprite, 2f); // Adjust delay as needed
        UseAbility(door);

    }

    [CanBeNull] IAbility GetNextAbility() => availableAbilitiesForRound.FirstOrDefault();
}