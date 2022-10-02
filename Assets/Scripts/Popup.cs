using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// ポップアップ制御
/// （リザルト画面とゲームオーバー画面）
/// </summary>
public class Popup : MonoBehaviour
{

    [SerializeField] double FadeOutStart; // 音楽フェードアウト開始タイミング(秒)
    [SerializeField] double FadeOutEnd;  // 音楽フェードアウト終了タイミング(秒)

    [SerializeField] GameObject ResultPopUp; // リザルトのポップアップ画面
    [SerializeField] GameObject GameOverPopUp; // ゲームオーバーのポップアップ画面

    // リザルト画面用 ///////

    [SerializeField] GameObject[] HeartList; // ハート3つのリスト
    [SerializeField] Sprite HalfHeart; // 半分のハートの画像
    [SerializeField] Sprite EmptyHeart; // 完全になくなったハートの画像

    [SerializeField] TextMeshProUGUI scoreText; // スコアテキスト
    [SerializeField] TextMeshProUGUI ClearJudgeText; // クリア判定テキスト

    [SerializeField] Slider TankGauge; // タンクの水量ゲージ
    [SerializeField] TextMeshProUGUI TankPerText; // 水量の割合(%)

    /////////////////////////

    AudioSource audioSource; // オーディオソース（BGMフェードアウトに利用）

    // 別スクリプト参照用
    TankManager tankManager; 
    BucketController bucketController;

    double FadeSpan; 
    double actualFadeOutStart;
    double actualFadeOutEnd;

    bool IsFadeEnded = false;
    double FadeDeltaTime = 0;
    bool IsResultPopuped = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        FadeSpan = FadeOutEnd - FadeOutStart;
        actualFadeOutStart = FadeOutStart;
        actualFadeOutEnd = FadeOutEnd;

        // はじめはポップアップを消しておく
        ResultPopUp.SetActive(false);
        GameOverPopUp.SetActive(false);

        tankManager = GameObject.Find("Tank").GetComponent<TankManager>();
        bucketController = GameObject.Find("Bucket").GetComponent<BucketController>();
    }

    public bool GetIsResultPopuped(){
        return IsResultPopuped;
    }


    /// <summary>
    /// リザルトポップアップの表示
    /// </summary>
    void ResultPopup(){
        // リザルトポップアップ表示
        ResultPopUp.SetActive(true);

        // ハート表示
        // ハート3つでHP1～6を表す
        // 例：HP3 -> () (     
        //     HP5 -> () () (
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

        // タンクの水量の割合(%)
        int allDropNum = tankManager.getAllDropNum();
        int tankPer = score*100 / allDropNum;
        TankPerText.text = $"{tankPer}";

        // タンクの水量ゲージ
        TankGauge.value = tankPer/100f;

        // クリア判定
        if (tankPer >= 100){
            ClearJudgeText.text = "FULLTANK!!";
        }
        else if (tankPer >= 75){
            ClearJudgeText.text = "CLEAR!";
        }
        else {
            ClearJudgeText.text = "FAIL";
        }
    }

    /// <summary>
    /// BGMのフェードアウト
    /// </summary>
    void Fade(){
        // フェードアウト開始
        if (FadeDeltaTime >= actualFadeOutStart){
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