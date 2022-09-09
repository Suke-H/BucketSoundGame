using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] GameObject[] HeartList;
    [SerializeField] Sprite FullHeart;
    [SerializeField] Sprite EmptyHeart;
    [SerializeField] Sprite Q1Tank;
    [SerializeField] Sprite Q2Tank;

    BucketController bucketController;
    
    // 残基
    private int HP = 3;
    private int score = 0;

    void Start(){
        bucketController = GameObject.Find("Bucket").GetComponent<BucketController>();
    }

    void Update()
    {
        // タンクにネズミやカラスがぶつかる -> 敵消す + ハートを1減らす
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, new Vector2(width,height), 0, Vector2.zero);
        if (hit2D){
            Destroy(hit2D.collider.gameObject);
            if (HP > 0){
                HeartList[3-HP].GetComponent<SpriteRenderer>().sprite = EmptyHeart;
                HP--;
            }
        }

        score = bucketController.getScore();

        // タンク
        if (score >= 5){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Q1Tank;
        }

        if (score >= 10){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Q2Tank;
        }
    }

    // 可視化ツール
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(width, height, 1));
    }
}
