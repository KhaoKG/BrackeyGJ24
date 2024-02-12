using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    public List<IAbility> availableAbilities;
    public IAbility activeAbility;

    public void ActivateAbility(int index)
    {
        if (index >= 0 && index < availableAbilities.Count)
        {
            availableAbilities[index].Activate();
            activeAbility = availableAbilities[index];
        }
    }

    public void ActivateAbility(KeySO key)
    {
        var ability = availableAbilities.Find(ability => ability.GetAbilitySO().Name
            .Equals(key.Ability.Name));
        ability.Activate();
        activeAbility = ability;
    }

    public void DisableAbility(int index)
    {
        if (index >= 0 && index < availableAbilities.Count)
        {
            availableAbilities[index].Deactivate();
            activeAbility = null;
        }
    }

    public void DisableAbility(KeySO key)
    {
        var ability = availableAbilities.Find(ability => ability.GetAbilitySO().Name
            .Equals(key.Ability.Name));
        ability.Deactivate();
        activeAbility = null;
    }

    public void AddAbility(IAbility ability)
    {
        availableAbilities.Add(ability);
    }
}