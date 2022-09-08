using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketController : MonoBehaviour
{

    [SerializeField] float radius;
    
    private RaycastHit2D hit2D;

    void Start()
    {
        hit2D = Physics2D.CircleCast(transform.position, radius, Vector3.zero);
    }

    void Update()
    {
        if (hit2D){
            Debug.Log("hit!!!");

            Destroy(hit2D.collider.gameObject);
        }
    }

    // 可視化ツール
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
