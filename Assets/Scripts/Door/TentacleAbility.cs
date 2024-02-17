using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleAbility : MonoBehaviour, IAbility {

    public AbilitySO abilityData;

    [SerializeField]
    GameObject tentaclePrefab;
    GameObject spawnedTentacle;

    GameObject door;

    public GameObject Door { get => door; set => door = value; }

    public void Activate(GameObject door) {
        // Spawn tentacle
        AkSoundEngine.PostEvent("doorTentacleLoom", this.gameObject);
        spawnedTentacle = Instantiate(tentaclePrefab, door.transform.position, Quaternion.identity);

        Tentacle tentacleScript = spawnedTentacle.GetComponentInChildren<Tentacle>();
        tentacleScript.Ability = this;
        tentacleScript.RotateAccordingToDoor(door.GetComponent<DoorEventManager>().DoorId);

        // Keep track of its door
        this.door = door;
    }

    public void Deactivate(GameObject door) {
        // Tentacle already dies by itself after its animation and particles
        door.GetComponent<DoorEventManager>().isUsingAbility = false;
        door.GetComponent<Animator>().SetBool("doorOpen", false);
    }


    public AbilitySO GetAbilitySo() => abilityData != null ? abilityData : Resources.Load<AbilitySO>("ScriptableObjects/Tentacle");
}
