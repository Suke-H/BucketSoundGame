using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 練習ステージで、しずくと敵を時間経過で生成させるスクリプト
/// </summary>
public class Practice : MonoBehaviour
{
    [SerializeField] private float waterSpan; // しずくの生成スパン
    [SerializeField] private float enemySpan; // 敵の生成スパン
    [SerializeField] GameObject Water; // しずく
    [SerializeField] GameObject[] enemyPrefabList; // カラスとネズミ
    [SerializeField] GameObject[] waterPosList; // しずくが落ちてくる場所（上側に左・中・右の3か所）
    [SerializeField] GameObject[] enemyPosList; // 敵が生成される場所（左側に上・下の2か所）

    float delta_1 = 0;
    float delta_2 = 0;

    /// <summary>
    /// しずく生成
    /// </summary>
    void FallWater()
    {
        GameObject water = Instantiate(Water);
        int num = Random.Range(0, 3);
        water.transform.position = waterPosList[num].transform.position;
    }

    /// <summary>
    /// 敵生成
    /// </summary>
    public void Spawn()
    {
        int num = Random.Range(0, 2);
        GameObject enemy = Instantiate(enemyPrefabList[num]);
        enemy.transform.position = enemyPosList[num].transform.position;
    }

    void Update()
    {

        // 時間経過でしずく、敵を生成
        
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
