using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/Enemy Wave")]
public class WaveSO : ScriptableObject
{
    [Serializable]
    public class TimeSpawn {
        [Min(0)]
        public float delay;
        public List<EnemySpawnEnum> enemies;
    }

    public int waveNumber = 1;

    public List<TimeSpawn> spawns;

    private void OnValidate() {
        // Guarantee that waves have some time delay between them
        for (int i = 1; i < spawns.Count; i++) {
            if (spawns[i].delay == 0) {
                Debug.LogError("<b>Error in " + name + "</b>: Only the first spawn can have 0 delay!");
            }
        }
    }

}
