using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityKey", menuName = "ScriptableObjects/AbilityKey")]
public class KeySO : ScriptableObject
{
    public string Name;
    public Sprite IconSprite;
    public AbilitySO Ability;
}
