using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public void Activate(GameObject door);
    public void Deactivate(GameObject door);
    public AbilitySO GetAbilitySo();
}