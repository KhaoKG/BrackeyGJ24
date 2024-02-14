using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractPoint : MonoBehaviour
{
    public bool isOn = false;
    GameObject windEffect;

    // Start is called before the first frame update
    void Start()
    {
        windEffect = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // get all nearby objects

    }

    public void TurnOn()
    {
        isOn = true;
        windEffect.SetActive(true);
    }

    public void TurnOff()
    {
        isOn = false;
        windEffect.SetActive(false);
    }
}
