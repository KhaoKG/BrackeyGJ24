using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class KeySelection : MonoBehaviour {
    GameStateSO gameStateData;

    [SerializeField] 
    FadeEffect screenFadeEffect;

    [SerializeField] GameObject door;
    [SerializeField] GameObject doorBack;

    private void Start() {
        // Load game data
        gameStateData = Resources.Load<GameStateSO>("ScriptableObjects/MainGameData");

        // Add a key for each child
        //the four options
        List<string> keys = new List<string>();
        keys.Add("Laser");
        keys.Add("Vacuum");
        keys.Add("Tentacle");
        keys.Add("Hell Portal");
        keys.Add("HP Up");

        // randomly assign three
        for (int i = 0; i < 3; i++)
        {
            // get a key
            string chosenKey = keys[Random.Range(0, keys.Count)];
            keys.Remove(chosenKey);
            transform.GetChild(i).GetComponent<OnHoverScale>().chosenKey = chosenKey;

            // set the right color
            if (chosenKey == "Laser")
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.magenta;
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "LASER\nSummon a giant laser to fire across the room.";

            }
            else if (chosenKey == "Hell Portal")
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.red;
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "PORTAL\nOpen a portal to a dimension and summon a chaotic entity that attacks anyone in sight.";
            }
            else if (chosenKey == "Vacuum")
            {
                //transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.magenta;
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "VACUUM\nOpen a vortex to suck up everything nearby.";
            }
            else if (chosenKey == "Tentacle")
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(0.47f, 0, 1, 1);
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "KRAKEN\nOpen the door in front of a very angry Kraken.";
            }
            else if (chosenKey == "HP Up")
            {
                transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.green;
                transform.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = "HP UP\nThis key will just upgrade your HP :)";
            }


        }
    }

    public void KeySelected(string Key) {

        // send the key
        if(Key != "HP Up")
        {
            AbilityController.Instance.UnlockAbility(Key);
        }
        else
        {
            gameStateData.maxHealth += 2;
        }

        // Return to fight next wave
        StartCoroutine(PrepareNextWave());
    }

    IEnumerator PrepareNextWave() {
        // Activate game object to fade screen
        //screenFadeEffect.TargetAlpha = 1f;

        //yield return new WaitForSeconds(1f / screenFadeEffect.FadeSpeed);

        door.SetActive(true);
        doorBack.SetActive(true);
        Animator doorAnimator = door.GetComponent<Animator>();

        for(int i=0; i < 3; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        GetComponent<Image>().enabled = false;

        AkSoundEngine.PostEvent("PlayButton", this.gameObject);
        doorAnimator.SetTrigger("StartGame");
        yield return new WaitForSeconds(1.5f);

        // zoom camera
        for (int i = 0; i < 50; i++)
        {
            Camera.main.orthographicSize -= 0.1f;
            yield return new WaitForSeconds(0.01f);
        }

        GoToNextWave();
    }

    void GoToNextWave() {
        gameStateData.currentWave++;
        SceneManager.LoadScene(2);
    }
}
