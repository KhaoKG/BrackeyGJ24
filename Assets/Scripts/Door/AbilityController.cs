using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class AbilityController : Singleton<AbilityController>
{
    public List<IAbility> availableAbilitiesForRound = new(); // This holds the abilities for this round
    public List<IAbility> totalAvailableAbilities = new(); // This holds the total abilities the player unlocked in the run
    public IAbility activeAbility; // Utilitary, if needed
    public AbilityListSO abilitiesSo; // Total abilities in the game. Distinct.
    private bool isNextAbilityUsable = true;

    public GameObject HellPortalPrefab;
    public GameObject VascuumPrefab;
    public GameObject TentaclePrefab;
    public GameObject LaserPortalPrefab;

    public void Start()
    {
        HellPortalPrefab = Resources.Load<GameObject>("Prefabs/HellPortal");
        VascuumPrefab = Resources.Load<GameObject>("Prefabs/Vacuum");
        abilitiesSo = Resources.Load<AbilityListSO>("ScriptableObjects/AbilityList");
        PopulateAvailableAbilities();
        UpdateAbilitiesForRound();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UseAbility();
        }
    }
    #region Activate abilities
    public void ActivateAbility(int index, Vector3 position)
    {
        if (index >= 0 && index < availableAbilitiesForRound.Count)
        {
            var nextAbility = availableAbilitiesForRound[index];
            var abilityObj = Instantiate(nextAbility.GetAbilitySo().AbilityPrefab, position, Quaternion.identity);
            abilityObj.GetComponent<IAbility>().Activate();
            activeAbility = nextAbility;
            isNextAbilityUsable = false;
        }
    }

    public void ActivateAbility(IAbility ability, Vector3 position)
    {
        var abilityObj = Instantiate(ability.GetAbilitySo().AbilityPrefab, position, Quaternion.identity);
        abilityObj.GetComponent<IAbility>().Activate();
        activeAbility = ability;
        isNextAbilityUsable = false;
    }

    public void ActivateNextAbility(Vector3 position)
    {
        var nextAbility = availableAbilitiesForRound.First();
        Debug.Log(nextAbility.GetAbilitySo().Name);
        var abilityObj = Instantiate(nextAbility.GetAbilitySo().AbilityPrefab, position, Quaternion.identity);
        abilityObj.GetComponent<IAbility>().Activate();
        activeAbility = nextAbility;
        isNextAbilityUsable = false;
    }

    #endregion
    public void DisableAbility(int index)
    {
        if (index >= 0 && index < availableAbilitiesForRound.Count)
        {
            availableAbilitiesForRound[index].Deactivate();
            activeAbility = null;
            RemoveAbility(index);
        }
    }

    #region Disable abilities ( consume )


    public void DisableAbility(IAbility ability)
    {
        ability.Deactivate();
        activeAbility = null;
        RemoveAbility(ability);
        isNextAbilityUsable = true;
    }

    public void DisableNextAbility()
    {
        availableAbilitiesForRound.First().Deactivate();
        activeAbility = null;
        if(availableAbilitiesForRound.Count > 0)
        {
            availableAbilitiesForRound.RemoveAt(0);
            isNextAbilityUsable = true;
        }
        else
        {
            Debug.LogError("Trying to remove an ability that does not exist");
        }
    }

    #endregion

    /// <summary>
    /// Add ability should be used at the end of round screen where you receive a key
    /// </summary>
    /// <param name="ability"></param>
    public void AddAbility(IAbility ability)
    {
        totalAvailableAbilities.Add(ability);
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
        availableAbilitiesForRound.AddRange(totalAvailableAbilities);
    }

    /// <summary>
    /// Add here all the abilities you make
    /// </summary>
    public void PopulateAvailableAbilities()
    {
        totalAvailableAbilities.Clear();
        abilitiesSo.Abilities.ForEach(ability =>
        {
            switch (ability.Name)
            {
                case "Hell Portal":
                    totalAvailableAbilities.Add(HellPortalPrefab.GetComponent<HellPortal>());
                    break;
                case "Vacuum":
                    totalAvailableAbilities.Add(VascuumPrefab.GetComponent<Vacuum>());
                    break;
                default:
                    break;
            }
        });
    }

    void UseAbility()
    {
        if (isNextAbilityUsable && availableAbilitiesForRound.Any())
        {
            Debug.Log("Activate");
            //TODO: Add current door position
            ActivateNextAbility(Vector2.zero);
            availableAbilitiesForRound.RemoveAt(0);
            //TODO: Refresh UI ( remove a key icon)
        }
    }
}