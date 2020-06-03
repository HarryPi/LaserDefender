using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<WaveConfig> waveConfigs;

    [SerializeField]
    private int startingWave;
    // Start is called before the first frame update
    void Start() {
        WaveConfig currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemies(currentWave));
    }

    private IEnumerator SpawnAllEnemies(WaveConfig currentWave) {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++) {
            Instantiate(currentWave.EnemyPrefab, currentWave.GetWayPoints().First().transform.position, Quaternion.identity);
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }
    }

}