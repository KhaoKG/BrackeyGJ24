using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellPortal : MonoBehaviour, IAbility
{
    public AbilitySO abilityData;
    public GameObject EnemyPrefab;
    private GameObject spawnedEnemy;
    private LineRenderer lineRenderer; // Used to draw the link between the portal and the enemy

    private void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.positionCount = 2; // Only two points needed
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.05f; // Adjust the line width as needed

        lineRenderer.sortingLayerName = "Foreground";
    }
    public void Activate(GameObject door)
    {
        if (EnemyPrefab != null)
        {
            spawnedEnemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            AkSoundEngine.PostEvent("doorKnightSpawn", this.gameObject);
        }

        // Start drawing the link if the enemy is spawned
        if (spawnedEnemy != null)
        {
            StartCoroutine(ActivateAndDeactivateCoroutine(door));
            StartCoroutine(UpdateLineRenderer());
        }
    }

    public void Deactivate(GameObject door)
    {
        StopAllCoroutines(); // Stop all coroutines when deactivating

        lineRenderer.enabled = false;

        // Destroy the spawned enemy
        if (spawnedEnemy != null)
        {
            Destroy(spawnedEnemy);
            spawnedEnemy = null;
            AkSoundEngine.PostEvent("doorKnightDie", this.gameObject);
        }

        door.GetComponent<DoorEventManager>().isUsingAbility = false;
        door.GetComponent<Animator>().SetBool("doorOpen", false);
    }

    private IEnumerator UpdateLineRenderer()
    {
        lineRenderer.enabled = true;
        while (spawnedEnemy != null)
        {
            lineRenderer.SetPosition(0, transform.position); // Portal position
            lineRenderer.SetPosition(1, spawnedEnemy.transform.position); // Enemy position

            yield return null;
        }
    }

    private IEnumerator ActivateAndDeactivateCoroutine(GameObject door)
    {
        yield return new WaitForSeconds(abilityData.ActiveTime); // Wait for 30 seconds
        Deactivate(door); // Deactivate after 30 seconds
    }

    public AbilitySO GetAbilitySo() => abilityData != null ? abilityData : Resources.Load<AbilitySO>("ScriptableObjects/HellPortal");
}
