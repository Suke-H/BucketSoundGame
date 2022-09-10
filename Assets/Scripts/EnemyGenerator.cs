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

}