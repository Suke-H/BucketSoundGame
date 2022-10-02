using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// しずくと敵（カラスとネズミ）生成
/// </summary>
public class NotesGenerator : MonoBehaviour
{
    [SerializeField] GameObject Water; // しずく
    [SerializeField] GameObject[] enemyPrefabList; // カラスとネズミ
    [SerializeField] GameObject[] waterPosList; // しずくが落ちてくる場所（上側に左・中・右の3か所）
    [SerializeField] GameObject[] enemyPosList; // 敵が生成される場所（左側に上・下の2か所）

    /// <summary>
    /// しずく生成
    /// </summary>
    public void FallWater()
    {
        GameObject water = Instantiate(Water); // しずく生成

        // しずくが落ちる場所を3か所からランダム選択
        int num = Random.Range(0, 3);
        water.transform.position = waterPosList[num].transform.position;
    }

    /// <summary>
    /// 敵生成
    /// </summary>
    public void Spawn()
    {
        // カラスとネズミをランダム生成
        int num = Random.Range(0, 2);
        GameObject enemy = Instantiate(enemyPrefabList[num]);

        // カラスなら上、ネズミなら下から生成
        enemy.transform.position = enemyPosList[num].transform.position;
    }

}