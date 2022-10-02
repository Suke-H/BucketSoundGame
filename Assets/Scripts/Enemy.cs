using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カラスとネズミ用
/// </summary>
public class Enemy : MonoBehaviour
{
    void Update()
    {
        // 移動
        transform.Translate(10f*Time.deltaTime, 0, 0);
    }
}
