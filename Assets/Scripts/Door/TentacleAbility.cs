using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAbility : MonoBehaviour, IAbility {

    public AbilitySO abilityData;

    [SerializeField]
    GameObject tentaclePrefab;
    GameObject spawnedTentacle;


    public void Activate(GameObject door) {
        // Spawn tentacle
        spawnedTentacle = Instantiate(tentaclePrefab, door.transform);
        spawnedTentacle.transform.localPosition = Vector3.zero;
    }

    public void Deactivate(GameObject door) {
        // Tentacle already dies by itself after its animation and particles
    }

    public AbilitySO GetAbilitySo() => abilityData;
}
