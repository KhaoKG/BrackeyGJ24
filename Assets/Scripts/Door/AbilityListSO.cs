using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityList", menuName = "ScriptableObjects/AbilityList")]
public class AbilityListSO : ScriptableObject
{
    public List<AbilitySO> Abilities;
}