using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    SpriteRenderer sr;
    public string keyType;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        float keyIndex = Random.Range(0, 100);
        if(keyIndex <= 25)
        {
            sr.color = Color.magenta;
            keyType = "Laser";
        }
        else if(keyIndex <= 50)
        {
            keyType = "Vacuum";
        }
        else if(keyIndex <= 75)
        {
            sr.color = Color.red;
            keyType = "Hell Portal";
        }
        else
        {
            sr.color = new Color(0.47f, 0, 1, 1);
            keyType = "Tentacle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
