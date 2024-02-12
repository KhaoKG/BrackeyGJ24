using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void DealDamage();
    public void Activate();
    public void Deactivate();
    public AbilitySO GetAbilitySO();
}