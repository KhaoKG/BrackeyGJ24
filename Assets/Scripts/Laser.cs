using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IAbility
{
    [SerializeField] AbilitySO SO;
    [SerializeField] GameObject warning;

    public void Activate(GameObject door)
    {
        transform.position = door.transform.position;

        if (door.GetComponent<DoorEventManager>().DoorId == 1) // left
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else if (door.GetComponent<DoorEventManager>().DoorId == 2) // top
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
            warning.transform.localRotation = Quaternion.Euler(0, 0, -90);
        }
        else if (door.GetComponent<DoorEventManager>().DoorId == 3) // right
        {
            //laser.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (door.GetComponent<DoorEventManager>().DoorId == 0) // bottom
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            warning.transform.localRotation = Quaternion.Euler(0, 0, -270);
        }
        StartCoroutine(ActivateAndDeactivateCoroutine(door));
    }

    public void Deactivate(GameObject door)
    {
        door.GetComponent<DoorEventManager>().isUsingAbility = false;
        door.GetComponent<Animator>().SetBool("doorOpen", false);
        Destroy(gameObject);
    }

    public AbilitySO GetAbilitySo() => SO != null ? SO : Resources.Load<AbilitySO>("ScriptableObjects/Laser");

    public void OnDestroy()
    {
        AkSoundEngine.PostEvent("doorLaserStop", this.gameObject);
    }
    private IEnumerator ActivateAndDeactivateCoroutine(GameObject door)
    {
        //AkSoundEngine.PostEvent("doorLaserWarn", this.gameObject);
        yield return new WaitForSeconds(0.5f);
        //AkSoundEngine.PostEvent("doorLaserFire", this.gameObject);
        yield return new WaitForSeconds(SO.ActiveTime);
        //AkSoundEngine.PostEvent("doorLaserStop", this.gameObject);
        Deactivate(door);
    }

    public void Warning ()
    {
        AkSoundEngine.PostEvent("doorLaserWarn", this.gameObject);
    }

    public void LightItUp()
    {
        AkSoundEngine.PostEvent("doorLaserFire", this.gameObject);
    }
}
