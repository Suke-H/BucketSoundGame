using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    void Update()
    {
        // 落下
        transform.Translate(0.03f, 0, 0);

        // ある程度下まで行ったら消える
        if (transform.position.x > 10) {
            Destroy(this.gameObject);
        }
    }
}
