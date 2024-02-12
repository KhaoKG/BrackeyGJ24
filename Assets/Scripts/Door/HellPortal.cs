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
    [ContextMenu("Activate Hell Portal")]
    public void ActivateForDebug()
    {
        Activate();
    }

    [ContextMenu("Deactivate Hell Portal")]
    public void DeactivateForDebug()
    {
        Deactivate();
    }

    public void Activate()
    {
        if (EnemyPrefab != null)
        {
            spawnedEnemy = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        }

        // Start drawing the link if the enemy is spawned
        if (spawnedEnemy != null)
        {
            StartCoroutine(UpdateLineRenderer());
        }
    }

    public void Deactivate()
    {
        StopCoroutine(UpdateLineRenderer());
        lineRenderer.enabled = false;

        // Destroy the spawned enemy
        if (spawnedEnemy != null)
        {
            Destroy(spawnedEnemy);
            spawnedEnemy = null;
        }
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
    public AbilitySO GetAbilitySo() => abilityData;
}
