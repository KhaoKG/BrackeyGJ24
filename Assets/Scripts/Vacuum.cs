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
        attractPoint.transform.position = door.transform.position;

        if (door.GetComponent<DoorEventManager>().DoorId == 1) // left
        {
            attractPoint.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (door.GetComponent<DoorEventManager>().DoorId == 2) // top
        {
            //transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (door.GetComponent<DoorEventManager>().DoorId == 3) // right
        {
            attractPoint.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (door.GetComponent<DoorEventManager>().DoorId == 0) // bottom
        {
            attractPoint.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        }
        StartCoroutine(ActivateAndDeactivateCoroutine(door));

        attractPoint.GetComponent<AttractPoint>().TurnOn();
        StartCoroutine(ActivateAndDeactivateCoroutine(door));
    }

    public void Deactivate(GameObject door)
    {
        AkSoundEngine.PostEvent("doorVacuumStop", this.gameObject);
        attractPoint.GetComponent<AttractPoint>().TurnOff();
        attractPoint.transform.localScale = new Vector3(1, 1, 1);
        attractPoint.transform.rotation = Quaternion.identity;
        StopAllCoroutines();
        door.GetComponent<DoorEventManager>().isUsingAbility = false;
        door.GetComponent<Animator>().SetBool("doorOpen", false);
    }

    public AbilitySO GetAbilitySo() => SO != null ? SO : Resources.Load<AbilitySO>("ScriptableObjects/Vacuum");

    private IEnumerator ActivateAndDeactivateCoroutine(GameObject door)
    {
        yield return new WaitForSeconds(SO.ActiveTime);
        Deactivate(door);
    }
}
