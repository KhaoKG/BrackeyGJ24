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
        Debug.Log("key spawned");

        sr = GetComponent<SpriteRenderer>();
        float keyIndex = Random.Range(0, AbilityController.Instance.typesOfAbilitesUnlocked.Count * 25);

        Debug.Log("unlocked abilities count: " + AbilityController.Instance.typesOfAbilitesUnlocked.Count);
        Debug.Log("key index: " + keyIndex);

        if (keyIndex <= 0)
        {
            Destroy(gameObject);
        }
        else if(keyIndex <= 25)
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[0].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[0].GetAbilitySo().Name;
        }
        else if(keyIndex <= 50)
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[1].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[1].GetAbilitySo().Name;
        }
        else if(keyIndex <= 75)
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[2].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[2].GetAbilitySo().Name;
        }
        else
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[3].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[3].GetAbilitySo().Name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
