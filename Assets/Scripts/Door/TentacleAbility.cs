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
        AkSoundEngine.PostEvent("doorTentacleLoom", this.gameObject);
        spawnedTentacle = Instantiate(tentaclePrefab, door.transform);
        spawnedTentacle.transform.localPosition = Vector3.zero;

        spawnedTentacle.GetComponentInChildren<Tentacle>().RotateAccordingToDoor(door.GetComponent<DoorEventManager>().DoorId);
    }



    public void Deactivate(GameObject door) {
        // Tentacle already dies by itself after its animation and particles
    }

    public AbilitySO GetAbilitySo() => abilityData;
}
