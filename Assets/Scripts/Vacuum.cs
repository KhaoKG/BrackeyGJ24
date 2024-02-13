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
        attractPoint.GetComponent<AttractPoint>().isOn = true;
    }

    public void Deactivate()
    {
        attractPoint.GetComponent<AttractPoint>().isOn = false;
    }

    public AbilitySO GetAbilitySo() => SO != null ? SO : Resources.Load<AbilitySO>("ScriptableObjects/Vacuum");
}
