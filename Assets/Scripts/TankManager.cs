using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タンクの制御（ハート/水量/ゲームオーバー処理）
/// </summary>
public class TankManager : MonoBehaviour
{
    // 当たり判定の直方体の幅(x)、高さ(y)
    [SerializeField] float width;
    [SerializeField] float height;

    [SerializeField] GameObject[] HeartList; // ハート3つのリスト
    [SerializeField] Sprite HalfHeart; // 半分のハートの画像
    [SerializeField] Sprite EmptyHeart; // 完全になくなったハートの画像

    // タンクの水量に応じて画像変更
    // (0/4, 1/4, 2/4, 3/4, 4/4の5つの画像)
    [SerializeField] Sprite[] QuarterTank; 

    [SerializeField] int allDropNum; // 全しずく数

    // 別スクリプト参照用
    BucketController bucketController;
    
    private int HP = 6; // タンクのHP（初期は6）
    private int score = 0; // スコア
    int[] quarterScore = new int[4]; 
    int quota; // ノルマ

    /// <summary>
    /// HPを1減らす処理
    /// </summary>
    void reduceHeart(){
        HP--; // HPを1減らす

        // ハートの表示処理
        // 例：HP3 -> () (     
        //     HP5 -> () () (
        int heartNo = (5 - HP) / 2; 
        int oneHeartStatus = HP % 2;
        if (oneHeartStatus == 0){
            HeartList[heartNo].GetComponent<SpriteRenderer>().sprite = EmptyHeart;
        }
        else {
            HeartList[heartNo].GetComponent<SpriteRenderer>().sprite = HalfHeart;
        }
    }

    public int getHP(){
        return HP;
    }

    public int getAllDropNum(){
        return allDropNum;
    }

    void Start(){
        bucketController = GameObject.Find("Bucket").GetComponent<BucketController>();

        // 4分割の水量のしきい値
        quarterScore[0] = allDropNum/4;
        quarterScore[1] = allDropNum/2;
        quarterScore[2] = allDropNum*3/4;
        quarterScore[3] = allDropNum;

        // ノルマは全しずく数の3/4
        quota = quarterScore[2];
    }

    void Update()
    {
        // タンクにネズミやカラスがぶつかる -> 敵消す + ハートを1減らす
        RaycastHit2D hit2D = Physics2D.BoxCast(transform.position, new Vector2(width,height), 0, Vector2.zero);
        if (hit2D){
            Destroy(hit2D.collider.gameObject);
            if (HP > 0){
                reduceHeart();
            }
        }

        // スコア更新
        score = bucketController.getScore();

        // スコアに応じてタンクの画像を切替
        for (int i=0; i<3; i++){
            if (quarterScore[i] <= score & score < quarterScore[i+1]){
                this.gameObject.GetComponent<SpriteRenderer>().sprite = QuarterTank[i];
            }
        }
        if (score >= allDropNum){
            this.gameObject.GetComponent<SpriteRenderer>().sprite = QuarterTank[3];
        }

    }

    // 可視化ツール
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector3(width, height, 1));
    }
}
