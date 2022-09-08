using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float waterSpan;
    [SerializeField] private float mouseSpan;
    [SerializeField] GameObject Water;
    [SerializeField] GameObject Mouse;
    [SerializeField] GameObject[] waterPosList;
    [SerializeField] GameObject mousePos;

    float delta_1 = 0;
    float delta_2 = 0;

    // スポーン
    void FallWater(GameObject Water)
    {
        GameObject water = Instantiate(Water);
        int num = Random.Range(0, 3);
        water.transform.position = waterPosList[num].transform.position;
    }

    void Spawn(GameObject enemyPrefab, GameObject enemyPos)
    {
        GameObject enemy = Instantiate(enemyPrefab);
        enemy.transform.position = enemyPos.transform.position;
    }

    void Update()
    {
        delta_1 += Time.deltaTime;
        delta_2 += Time.deltaTime;

        if (delta_1 > waterSpan){
            delta_1 = 0;
            FallWater(Water);
        }

        if (delta_2 > mouseSpan){
            delta_2 = 0;
            Spawn(Mouse, mousePos);
        }
    }
}