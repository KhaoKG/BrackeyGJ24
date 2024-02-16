using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class AbilitySelectionUi : Singleton<AbilitySelectionUi>
{
    public List<IAbility> AbilitiesToDisplay;
    public GameObject KeyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        KeyPrefab = Resources.Load<GameObject>("Prefabs/Key");
        AbilitiesToDisplay = AbilityController.Instance.availableAbilitiesForRound;
        Debug.Log("START ABILITYSELECTION");
        InstantiateAbilities();
    }

    void OnEnable()
    {
        Debug.Log("ENABLED ABILITYSELECTION");
    }

    // This function gets called when the AbilityUsed Action is triggered
    private void OnAbilityUsed(IAbility ability)
    {
        Debug.Log("Ability used: " + ability.GetAbilitySo().name);
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        else
        {
            Debug.LogWarning("No abilities to remove.");
        }
    }

    public void OnAbilitiesLoaded(List<IAbility> abilities)
    {
        AbilitiesToDisplay = abilities;
        InstantiateAbilities();
    }

    void InstantiateAbilities()
    {
        Debug.Log(AbilitiesToDisplay.Count + "total abilities for ui");
        if (transform.childCount > 0)
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        var abilities = AbilitiesToDisplay.Select(a => a.GetAbilitySo());
        foreach (AbilitySO ability in abilities)
        {
            var keyObject = Instantiate(KeyPrefab, transform);
            keyObject.SetActive(true);
            keyObject.GetComponent<Image>().sprite = ability.KeyIcon;
            keyObject.GetComponent<Image>().color = ability.KeyColor;
            // Set any necessary properties of the instantiated abilityObject here
        }
    }
}
