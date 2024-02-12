using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public List<IAbility> availableAbilitiesForRound; // This holds the abilities for this round
    public List<IAbility> totalAvailableAbilities; // This holds the total abilities the player unlocked in the run
    public IAbility activeAbility; // Utilitary, if needed

    #region Activate abilities
    public void ActivateAbility(int index)
    {
        if (index >= 0 && index < availableAbilitiesForRound.Count)
        {
            availableAbilitiesForRound[index].Activate();
            activeAbility = availableAbilitiesForRound[index];
        }
    }

    public void ActivateAbility(KeySO key)
    {
        var ability = availableAbilitiesForRound.Find(ability => ability.GetAbilitySo().Name
            .Equals(key.Ability.Name));
        ability.Activate();
        activeAbility = ability;
    }

    public void ActivateAbility(IAbility ability)
    {
        ability.Activate();
        activeAbility = ability;
    }

    public void ActivateNextAbility()
    {
       availableAbilitiesForRound.First().Activate();
       activeAbility = availableAbilitiesForRound.First();
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

    public void DisableAbility(KeySO key)
    {
        var ability = availableAbilitiesForRound.Find(ability => ability.GetAbilitySo().Name
            .Equals(key.Ability.Name));
        ability.Deactivate();
        activeAbility = null;
        RemoveAbility(key);
    }

    public void DisableAbility(IAbility ability)
    {
        ability.Deactivate();
        activeAbility = null;
        RemoveAbility(ability);
    }

    public void DisableNextAbility()
    {
        availableAbilitiesForRound.First().Activate();
        activeAbility = null;
        if(availableAbilitiesForRound.Count > 0)
        {
            availableAbilitiesForRound.RemoveAt(0);
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
    /// Add ability should be used at the end of round screen where you receive a key
    /// </summary>
    /// <param name="key"></param>
    public void AddAbility(KeySO key)
    {
        totalAvailableAbilities.Add(availableAbilitiesForRound
            .Find(a => a.GetAbilitySo().Name.Equals(key.Ability.Name)));
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

    public void RemoveAbility(KeySO key)
    {
        availableAbilitiesForRound.Remove(availableAbilitiesForRound
            .Find(a => a.GetAbilitySo().Name.Equals(key.Ability.Name)));
    }

    /// <summary>
    /// This should be called when the round changes, to refresh the active keys for the door.
    /// </summary>
    public void UpdateAbilitiesForRound()
    {
        availableAbilitiesForRound.Clear();
        availableAbilitiesForRound.AddRange(totalAvailableAbilities);
    }
}