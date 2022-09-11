using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Result : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] double FadeOutStart;
    [SerializeField] double FadeOutEnd;

    [SerializeField] GameObject ResultPopUp;
    [SerializeField] GameObject GameOverPopUp;

    [SerializeField] GameObject[] HeartList;
    [SerializeField] Sprite HalfHeart;
    [SerializeField] Sprite EmptyHeart;

    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] Slider TankGauge;
    [SerializeField] TextMeshProUGUI TankPerText;

    [SerializeField] TextMeshProUGUI ClearJudge;

    TankManager tankManager;
    BucketController bucketController;

    double FadeSpan;

    bool IsFadeEnded = false;
    double FadeDeltaTime = 0;
    bool IsResultPopuped = false;

    double actualFadeOutStart;
    double actualFadeOutEnd;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FadeSpan = FadeOutEnd - FadeOutStart;
        actualFadeOutStart =  FadeOutStart;
        actualFadeOutEnd = FadeOutEnd;

        // はじめは消しておく
        ResultPopUp.SetActive(false);
        GameOverPopUp.SetActive(false);

        tankManager = GameObject.Find("Tank").GetComponent<TankManager>();
        bucketController = GameObject.Find("Bucket").GetComponent<BucketController>();
    }

    public bool GetIsResultPopuped(){
        return IsResultPopuped;
    }

    void ResultPopup(){
        // 表示
        ResultPopUp.SetActive(true);

        // ハート表示
        int HP = tankManager.getHP();

        for (int i=0; i<3; i++){
            if (HP == 1) {
                HeartList[2-i].GetComponent<SpriteRenderer>().sprite = HalfHeart; 
            }
            else if(HP <= 0){
                HeartList[2-i].GetComponent<SpriteRenderer>().sprite = EmptyHeart; 
            }
            HP -= 2;
        }

        // スコア
        int score = bucketController.getScore();
        scoreText.text = $"{score}";

        // パーセント
        int allDropNum = tankManager.getAllDropNum();
        int tankPer = score*100 / allDropNum;
        TankPerText.text = $"{tankPer}";

        // タンクゲージ
        TankGauge.value = tankPer/100f;

        // クリア判定

        if (tankPer >= 100){
            ClearJudge.text = "FULLTANK!!";
        }

        else if (tankPer >= 75){
            ClearJudge.text = "CLEAR!";
        }

        else {
            ClearJudge.text = "FAIL";
        }

    }

    void Fade(){
         // フェードアウト開始
        if (FadeDeltaTime >= actualFadeOutStart){

            // フェードアウト
            audioSource.volume = (float)(1.0 - (FadeDeltaTime - actualFadeOutStart) / FadeSpan);
        }

        // フェードアウト終了
        if (FadeDeltaTime > actualFadeOutEnd){
            IsFadeEnded = true;
        }
        
    }

    void Update()
    {
        // フェードアウトが終わるまで
        if (!IsFadeEnded){
            FadeDeltaTime += Time.deltaTime;

            // ゲームオーバー画面表示
            int HP = tankManager.getHP();
            if (!IsResultPopuped & HP <= 0){
                GameOverPopUp.SetActive(true);
                IsResultPopuped = true;

                // フェード開始、終了時間を修正
                actualFadeOutStart = FadeDeltaTime;
                actualFadeOutEnd = actualFadeOutStart + FadeSpan;
            }

            // リザルト画面表示
            if (!IsResultPopuped & FadeDeltaTime >= actualFadeOutStart){
                ResultPopup();
                IsResultPopuped = true;
            }

            // 時間が来たらフェード
            Fade();
        }
    } 
}