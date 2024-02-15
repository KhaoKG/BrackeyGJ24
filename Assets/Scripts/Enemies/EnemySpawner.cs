using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Serializable]
    public class EnemyEnumPrefab {
        public EnemySpawnEnum enemyEnum;
        public GameObject prefab;
    }

    [Header("Enemies data")]
    [SerializeField] List<EnemyEnumPrefab> enemyMapping;
    // Uses the list to fill the dictionary for monster spawns, avoids searching a list every spawning
    Dictionary<EnemySpawnEnum, GameObject> enemies = new Dictionary<EnemySpawnEnum, GameObject>();
    [SerializeField] WaveSO currentWave;
    [SerializeField] float minSpawnDistance = 2f;
    [SerializeField] EnemyController enemyController;

    [Header("Player")]
    [SerializeField]
    Player player;

    [Header("Map")]
    [SerializeField]
    GameObject limits;

    Coroutine spawningCoroutine;

    public WaveSO CurrentWave { get => currentWave; set => currentWave = value; }

    void Awake() {
        // Prepares enemies dictionary
        foreach (EnemyEnumPrefab enemy in enemyMapping) {
            enemies[enemy.enemyEnum] = enemy.prefab;
        }

        // Frees list memory
        enemyMapping.Clear();

        // Deactivate to wait for starting signal from GameStateManager
        enabled = false;
    }

    private void OnEnable() {
        // Process wave as soon as script is enabled
        spawningCoroutine = StartCoroutine(ProcessCurrentWave());
    }

    public IEnumerator ProcessCurrentWave()
    {
        Debug.Log("I'm beginning spawning enemies");

        foreach (WaveSO.TimeSpawn spawn in currentWave.spawns) {
            yield return new WaitForSeconds(spawn.delay);

            foreach (EnemySpawnEnum enemyEnum in spawn.enemies) {
                Vector3 spawnPosition = DefineSpawnPosition();

                Enemy newEnemy = Instantiate(enemies[enemyEnum], spawnPosition, Quaternion.identity).GetComponent<Enemy>();
                enemyController.OnSpawnEnemy(newEnemy);

                // Gives enemies the player to track
                newEnemy.Player = player;
            }
        }

        enabled = false;
        spawningCoroutine = null;
    }

    public void StopSpawning() {
        if (spawningCoroutine != null) {
            StopCoroutine(spawningCoroutine); 
            spawningCoroutine = null;
        }
    }

    Vector3 DefineSpawnPosition() {
        // Get player position
        Vector3 playerPosition = player.transform.position;
        Vector3 spawnPoint = Vector3.zero;
        bool validSpawn = false;

        // Get random position in circle around player, then raycast to nearest map limit to define a position
        Vector3 spawnDirection = Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0, 360)) * Vector3.right;

        while (!validSpawn) {
            // Hit the closest wall
            RaycastHit2D[] hits = Physics2D.RaycastAll(playerPosition, spawnDirection);
            foreach (RaycastHit2D hit in hits) {
                if (hit.collider.CompareTag("MapLimits")) {
                    // Decrease max distance in order to avoid enemy spawning above collider
                    float maxDistance = hit.distance * 0.9f;

                    if (maxDistance > minSpawnDistance) {
                        spawnPoint = playerPosition + spawnDirection * UnityEngine.Random.Range(minSpawnDistance, maxDistance);
                        validSpawn = true;
                    }
                    else {
                        spawnDirection = Quaternion.Euler(Vector3.forward * 90f) * spawnDirection;
                    }
                    break;
                }
            }
        }

        return spawnPoint;
    }
}
