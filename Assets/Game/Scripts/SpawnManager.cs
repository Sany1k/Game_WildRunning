using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<ObjectPooler> pooledObstacles;

    private GameManager gameManager;
    private float spawnMinRate = 2f;
    private float spawnMaxRate = 3f;

    private void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(SpawnObstaclesRoutine());
        gameManager.NewStageEvent += OnSpawnRateChanger;
    }

    private IEnumerator SpawnObstaclesRoutine()
    {
        while (true)
        {
            float delay = Random.Range(spawnMinRate, spawnMaxRate);
            int rIndex = Random.Range(0, pooledObstacles.Count);

            yield return new WaitForSeconds(delay);
            SpawnObstacle(rIndex);
        }
    }

    private void SpawnObstacle(int index)
    {
        GameObject ob = pooledObstacles[index].GetPooledObjects();
        if (ob != null)
        {
            ob.SetActive(true);
            ob.transform.position = new Vector3(0, 0, 35f);
        }
    }

    private void OnSpawnRateChanger()
    {
        spawnMinRate /= 1.05f;
        spawnMaxRate /= 1.05f;
    }
}
