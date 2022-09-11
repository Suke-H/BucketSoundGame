using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Practice : MonoBehaviour
{
    [SerializeField] private float waterSpan;
    [SerializeField] private float enemySpan;
    [SerializeField] GameObject Water;
    [SerializeField] GameObject[] waterPosList;
    [SerializeField] GameObject[] enemyPrefabList;
    [SerializeField] GameObject[] enemyPosList;

    float delta_1 = 0;
    float delta_2 = 0;

    // スポーン
    void FallWater()
    {
        GameObject water = Instantiate(Water);
        int num = Random.Range(0, 3);
        water.transform.position = waterPosList[num].transform.position;
    }

    // スポーン
    public void Spawn()
    {
        int num = Random.Range(0, 2);
        GameObject enemy = Instantiate(enemyPrefabList[num]);
        enemy.transform.position = enemyPosList[num].transform.position;
    }

    void Update()
    {
        delta_1 += Time.deltaTime;
        delta_2 += Time.deltaTime;

        if (delta_1 > waterSpan){
            delta_1 = 0;
            FallWater();
        }

        if (delta_2 > enemySpan){
            delta_2 = 0;
            Spawn();
        }
    }
}
