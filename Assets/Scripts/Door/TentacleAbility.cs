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
        StartCoroutine(ActivateAndDeactivateCoroutine(door));
    }



    public void Deactivate(GameObject door) {
        // Tentacle already dies by itself after its animation and particles
        StopAllCoroutines();
        door.GetComponent<DoorEventManager>().isUsingAbility = false;
    }

    private IEnumerator ActivateAndDeactivateCoroutine(GameObject door)
    {
        yield return new WaitForSeconds(abilityData.ActiveTime);
        Deactivate(door);
    }

    public AbilitySO GetAbilitySo() => abilityData != null ? abilityData : Resources.Load<AbilitySO>("ScriptableObjects/Tentacle");
}
