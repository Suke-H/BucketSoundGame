using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    void Update()
    {
        // 落下
        transform.Translate(0, -8f*Time.deltaTime, 0);

        // ある程度下まで行ったら消える
        if (transform.position.y < -7) {
            Destroy(this.gameObject);
        }
    }
}
