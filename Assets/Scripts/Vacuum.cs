using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour, IAbility
{
    [SerializeField] AbilitySO SO;
    GameObject attractPoint;

    // Start is called before the first frame update
    void Start()
    {
        attractPoint = GameObject.FindGameObjectWithTag("Attract Point");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(GameObject door)
    {
        AkSoundEngine.PostEvent("doorVacuumEvent", this.gameObject);
        attractPoint = GameObject.FindGameObjectWithTag("Attract Point");
        attractPoint.GetComponent<AttractPoint>().TurnOn();
        StartCoroutine(ActivateAndDeactivateCoroutine(door));
    }

    public void Deactivate(GameObject door)
    {
        AkSoundEngine.PostEvent("doorVacuumStop", this.gameObject);
        attractPoint.GetComponent<AttractPoint>().TurnOff();
        StopAllCoroutines();
        door.GetComponent<DoorEventManager>().isUsingAbility = false;
    }

    public AbilitySO GetAbilitySo() => SO != null ? SO : Resources.Load<AbilitySO>("ScriptableObjects/Vacuum");

    private IEnumerator ActivateAndDeactivateCoroutine(GameObject door)
    {
        yield return new WaitForSeconds(SO.ActiveTime);
        Deactivate(door);
    }
}
