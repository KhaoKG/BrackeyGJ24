using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAbility : MonoBehaviour, IAbility {

    public AbilitySO abilityData;

    [SerializeField]
    GameObject tentaclePrefab;
    GameObject spawnedTentacle;

    [SerializeField]
    GameObject door;

    public GameObject Door { get => door; set => door = value; }


    [ContextMenu("Activate Tentacle")]
    public void ActivateForDebug() {
        Activate();
    }

    [ContextMenu("Deactivate Tentacle")]
    public void DeactivateForDebug() {
        Deactivate();
    }


    public void Activate() {
        // Spawn tentacle
        spawnedTentacle = Instantiate(tentaclePrefab, door.transform);
        spawnedTentacle.transform.localPosition = Vector3.zero;
    }

    public void Deactivate() {
        Destroy(spawnedTentacle);
    }

    public AbilitySO GetAbilitySo() => abilityData;
}
