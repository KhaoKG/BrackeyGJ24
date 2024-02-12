using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IAbility", menuName = "ScriptableObjects/IAbility")]
public class AbilitySO : ScriptableObject
{
    public string Name;
    public string Description;
    public double Cooldown;
    public double ActiveTime;
    public RuntimeAnimatorController AnimatorController;
    public GameObject AbilityPrefab
}
