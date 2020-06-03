using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField]
    private List<WaveConfig> waveConfigs;

    [SerializeField]
    private int startingWave = 0;

    [SerializeField]
    private bool loop = false;
    
    public IEnumerator Start() {
        do {
            yield return StartCoroutine(SpawnAllWaves());
        } while (loop);
    }

    private IEnumerator SpawnAllWaves() {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++) {
            WaveConfig currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }
    
    private IEnumerator SpawnAllEnemies(WaveConfig currentWave) {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++) {
            GameObject  newEnemy = Instantiate(currentWave.EnemyPrefab, currentWave.GetWayPoints().First().transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().WaveConfig = currentWave;
            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }
    }

}