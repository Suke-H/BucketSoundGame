using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float spawnSpan;
    [SerializeField] GameObject water;
    [SerializeField] GameObject[] enemyPosList;

    float delta = 0;

    // スポーン
    void Spawn(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        int num = Random.Range(0, 3);
        enemy.transform.position = enemyPosList[num].transform.position;
    }

    void Update()
    {
        delta += Time.deltaTime;

        if (delta > spawnSpan){
            delta = 0;
            Spawn(water);
        }
    }
}