using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void Activate();
    public void Deactivate();
    public AbilitySO GetAbilitySo();
}