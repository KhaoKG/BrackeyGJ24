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

    public void Activate()
    {
        attractPoint = GameObject.FindGameObjectWithTag("Attract Point");
        attractPoint.GetComponent<AttractPoint>().TurnOn();
        StartCoroutine(ActivateAndDeactivateCoroutine());
    }

    public void Deactivate()
    {
        attractPoint.GetComponent<AttractPoint>().TurnOff();
        StopAllCoroutines();
    }

    public AbilitySO GetAbilitySo() => SO != null ? SO : Resources.Load<AbilitySO>("ScriptableObjects/Vacuum");

    private IEnumerator ActivateAndDeactivateCoroutine()
    {
        yield return new WaitForSeconds(SO.ActiveTime);
        Deactivate();
    }
}
