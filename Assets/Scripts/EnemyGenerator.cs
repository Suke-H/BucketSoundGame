using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] private float waterSpan;
    [SerializeField] private float mouseSpan;
    [SerializeField] GameObject Water;
    [SerializeField] GameObject[] enemyPrefabList;
    [SerializeField] GameObject[] waterPosList;
    [SerializeField] GameObject[] enemyPosList;

    // スポーン
    public void FallWater()
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

    // void Update()
    // {
    //     delta_1 += Time.deltaTime;
    //     delta_2 += Time.deltaTime;

    //     // if (delta_1 > waterSpan){
    //     //     delta_1 = 0;
    //     //     FallWater(Water);
    //     // }

    //     if (delta_2 > mouseSpan){
    //         delta_2 = 0;
    //         Spawn(Mouse, mousePos);
    //     }
    // }
}