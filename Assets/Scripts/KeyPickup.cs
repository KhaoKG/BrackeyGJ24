using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    SpriteRenderer sr;
    public string keyType;

    [SerializeField] bool inTutorial;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("key spawned");

        sr = GetComponent<SpriteRenderer>();
        float keyIndex = Random.Range(0, AbilityController.Instance.typesOfAbilitesUnlocked.Count * 25);

        //Debug.Log("unlocked abilities count: " + AbilityController.Instance.typesOfAbilitesUnlocked.Count);
        //Debug.Log("key index: " + keyIndex);

        if (keyIndex <= 0)
        {
            if(!inTutorial)
            {
                Destroy(gameObject);
            }
            else
            {
                keyType = AbilityController.Instance.abilitiesSo.Abilities[0].Name;
                AkSoundEngine.PostEvent("keyDropped", this.gameObject);
            }
        }
        else if(keyIndex is > 0 and <= 25)
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[0].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[0].GetAbilitySo().Name;
            AkSoundEngine.PostEvent("keyDropped", this.gameObject);
        }
        else if(keyIndex is > 25 and <= 50)
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[1].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[1].GetAbilitySo().Name;
            AkSoundEngine.PostEvent("keyDropped", this.gameObject);
        }
        else if(keyIndex is > 50 and <= 75)
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[2].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[2].GetAbilitySo().Name;
            AkSoundEngine.PostEvent("keyDropped", this.gameObject);
        }
        else
        {
            sr.color = AbilityController.Instance.typesOfAbilitesUnlocked[3].GetAbilitySo().KeyColor;
            keyType = AbilityController.Instance.typesOfAbilitesUnlocked[3].GetAbilitySo().Name;
            AkSoundEngine.PostEvent("keyDropped", this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (AbilityController.Instance.availableAbilitiesForRound.Count >= 4)
            {
                return;
            }
            else
            {
                AbilityController.Instance.AddAbilityForRound(keyType);
                AkSoundEngine.PostEvent("playerKeyGain", this.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
