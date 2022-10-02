using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// バケツの移動やアニメーション制御
/// </summary>
public class BucketController : MonoBehaviour
{
    [SerializeField] float radius; // 当たり判定の半径
    [SerializeField] float changeSpan; // 水バケツが空に戻るまでのスパン
    [SerializeField] float inputWaitSpan; // バケツの移動キー入力後に（連打防止で）一定時間入力禁止するスパン
    [SerializeField] Sprite Bucket; // 空バケツの画像
    [SerializeField] Sprite FullBucket; // 水バケツの画像
    [SerializeField] GameObject[] bucketPosList; // 9マスのバケツ場所リスト
    [SerializeField] TextMeshProUGUI scoreText; // スコアテキスト

    private int score = 0; // スコア
    
    private bool fulledFlag = false; // バケツの画像が水バケツかどうか
    bool pushedFlag = false; // 移動キー入力禁止中かどうか
    private float delta_1 = 0;
    private float delta_2 = 0;

    // 現在位置
    private int currentPos = 4; // 初期位置は4（中央）
    // 0, 1, 2
    // 3, 4, 5
    // 6, 7, 8

    // scoreのgetter
    public int getScore(){
        return score;
    }

    /// <summary>
    /// バケツの移動
    /// 入力キー（↑←↓→）に応じてcurrentPosの値を変える
    /// ↑は-3, ←は-1, ↓は+3, →は+1
    /// 0, 1, 2
    /// 3, 4, 5
    /// 6, 7, 8
    /// 
    /// </summary>
    private void Move(){
        // 入力キーに応じた移動分（↑は-3, ←は-1, ↓は+3, →は+1）
        // 入力キーがなければ0
        int movePos = 0;

        // currentPosをx, yに分解
        int x = currentPos % 3;
        int y = currentPos / 3;

        // (↑←↓→, WASD)入力（入力禁止フラグがオフなら）
            
        // 左に移動
        if (Input.GetKey (KeyCode.LeftArrow) | Input.GetKey (KeyCode.A)) { 
            movePos = x > 0 ? -1 : 0; // x=0（左列）でなければ左に移動
            pushedFlag = true; // 入力禁止フラグ
        }
        // 右に移動
        if (Input.GetKey (KeyCode.RightArrow) | Input.GetKey (KeyCode.D)) { 
            movePos = x < 2 ? 1 : 0; // x=2（右列）でなければ右に移動
            pushedFlag = true;
        }
        // 上に移動
        if (Input.GetKey (KeyCode.UpArrow) | Input.GetKey (KeyCode.W)) { 
            movePos = y > 0 ? -3 : 0; // y=0（下段）でなければ上に移動
            pushedFlag = true;
        }
        // 下に移動
        if (Input.GetKey (KeyCode.DownArrow) | Input.GetKey (KeyCode.S)) { 
            movePos = y < 2 ? 3 : 0; // y=2（上段）でなければ下に移動
            pushedFlag = true;
        }

        // 移動を反映
        currentPos = currentPos + movePos;
        transform.position = bucketPosList[currentPos].transform.position;
    }

    void Update()
    {
        
        // バケツに当たり判定の球を生成
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, radius, Vector3.zero);
        if (hit2D){

            // バケツにしずくが当たる
            if (hit2D.collider.gameObject.name.Contains("Water")) {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = FullBucket; // 水入りバケツに変える
                fulledFlag = true; // 水バケツにする

                // スコアを増やす
                score++;
                scoreText.text = $"{score}"; 
            }

            Destroy(hit2D.collider.gameObject); // しずくを消す
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

        // バケツ移動（移動キー入力禁止待ちがある）
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

    // 当たり判定の可視化ツール
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
