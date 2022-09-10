using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BucketController : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] float changeSpan;
    [SerializeField] float inputWaitSpan;
    [SerializeField] Sprite Bucket;
    [SerializeField] Sprite FullBucket;
    [SerializeField] GameObject[] bucketPosList;
    [SerializeField] TextMeshProUGUI scoreText;

    // スコア
    private int score = 0;
    
    private bool fulledFlag = false;
    private float delta_1 = 0;
    private float delta_2 = 0;
    private int currentPos = 4;

    bool pushedFlag = false;

    public int getScore(){
        return score;
    }

    private void Move(){
        int movePos = 0;
        int x = currentPos % 3;
        int y = currentPos / 3;

        // (↑←↓→, WASD)入力（入力中フラグがオフなら）
            
        // 左に移動
        if (Input.GetKey (KeyCode.LeftArrow) | Input.GetKey (KeyCode.A)) { 
            movePos = x > 0 ? -1 : 0;
            pushedFlag = true;
        }
        // 右に移動
        if (Input.GetKey (KeyCode.RightArrow) | Input.GetKey (KeyCode.D)) { 
            movePos = x < 2 ? 1 : 0;
            pushedFlag = true;
        }
        // 上に移動
        if (Input.GetKey (KeyCode.UpArrow) | Input.GetKey (KeyCode.W)) { 
            movePos = y > 0 ? -3 : 0;
            pushedFlag = true;
        }
        // 下に移動
        if (Input.GetKey (KeyCode.DownArrow) | Input.GetKey (KeyCode.S)) { 
            movePos = y < 2 ? 3 : 0;
            pushedFlag = true;
        }

        // 移動
        currentPos = currentPos + movePos;
        transform.position = bucketPosList[currentPos].transform.position;
    }

    void Update()
    {
        // バケツにしずくが当たる -> しずくが消える + 水入りバケツに変わる
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, radius, Vector3.zero);
        if (hit2D){

            if (hit2D.collider.gameObject.name.Contains("Water")) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = FullBucket;
                fulledFlag = true;

                score++;
                scoreText.text = $"{score}";
            }

            Destroy(hit2D.collider.gameObject);
            delta_1 = 0;
        }

        // 一定時間で水入りバケツを元のバケツに
        if (fulledFlag){ 
            delta_1 += Time.deltaTime; 
        }
        if (delta_1 > changeSpan){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Bucket;
            delta_1 = 0;
            fulledFlag = false;
        }

        // 移動
        if (!pushedFlag){
            Move();
        }
        else {
            delta_2 += Time.deltaTime; 
            if (delta_2 > inputWaitSpan){
                delta_2 = 0;
                pushedFlag = false;
            }
        }

    }

    // 可視化ツール
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
