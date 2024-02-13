using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Ability")]
public class AbilitySO : ScriptableObject
{
    public string Name;
    public string Description;
    public float ActiveTime;
    public Sprite KeyIcon;
    public GameObject AbilityPrefab;
}