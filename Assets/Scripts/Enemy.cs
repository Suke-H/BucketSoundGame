using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    void Update()
    {
        // 移動
        transform.Translate(0.04f, 0, 0);

        // // ある程度下まで行ったら消える
        // if (transform.position.x > 10) {
        //     Destroy(this.gameObject);
        // }
    }
}
