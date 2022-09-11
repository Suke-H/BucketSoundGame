using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    [SerializeField] float width;
    [SerializeField] float height;
    [SerializeField] GameObject[] HeartList;
    [SerializeField] Sprite HalfHeart;
    [SerializeField] Sprite EmptyHeart;
    [SerializeField] Sprite[] QuarterTank;
    [SerializeField] int allDropNum;

    [SerializeField] GameObject GameOverPopUp;

    BucketController bucketController;
    // Result result;
    
    // 残基
    private int HP = 6;

    private int score = 0;
    int[] quarterScore = new int[4];
    int quota; // ノルマ

    void reduceHeart(){
        HP--;

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
        // result = GameObject.Find("Timeline").GetComponent<Result>();

        // 最初は消しておく
        // GameOverPopUp.SetActive(false);

        quarterScore[0] = allDropNum/4;
        quarterScore[1] = allDropNum/2;
        quarterScore[2] = allDropNum*3/4;
        quarterScore[3] = allDropNum;

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

        // HPが0になったらGAME OVER
        // (リザルト画面が出てたら、何もしない)
        // bool IsResultPopuped = result.GetIsResultPopuped();
        // if (!IsResultPopuped & HP <= 0){
        //     GameOverPopUp.SetActive(true);
        // }

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
