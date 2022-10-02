using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージ選択画面の制御（今回未使用）
/// </summary>
public class StageSelect : MonoBehaviour
{
    [SerializeField] GameObject[] stageNameList;
    [SerializeField] float inputWaitSpan;
    [SerializeField] Sprite[] whiteNames;
    [SerializeField] Sprite[] grayNames;

    public static int stageNo = 0;

    private float delta;
    private bool pushedFlag = false;

    public static int getStageNo(){
		return stageNo;
	}

    private void creditChange(){
        // 左に移動
        if (Input.GetKey (KeyCode.LeftArrow) | Input.GetKey (KeyCode.A)) { 
            pushedFlag = true;
            stageNo = 0;
        }

        // 右に移動
        if (Input.GetKey (KeyCode.RightArrow) | Input.GetKey (KeyCode.D)) { 
            pushedFlag = true;
            stageNo = 1;
        }

        // 移動
        transform.position = stageNameList[stageNo].transform.position;

        // 画像切替
        if (stageNo == 0) {
            stageNameList[0].GetComponent<SpriteRenderer>().sprite = whiteNames[0];
            stageNameList[1].GetComponent<SpriteRenderer>().sprite = grayNames[1];
        }
        else {
            stageNameList[0].GetComponent<SpriteRenderer>().sprite = grayNames[0];
            stageNameList[1].GetComponent<SpriteRenderer>().sprite = whiteNames[1];
        }

        // シーン遷移
        if (Input.GetKey (KeyCode.Return)) { 
            if (stageNo == 0){ SceneManager.LoadScene("Tutorial"); }
            else { SceneManager.LoadScene("Stage1"); }
        }

        // Escへタイトルへ
        if (Input.GetKey (KeyCode.Escape)) { 
            SceneManager.LoadScene("Title");
        }

    }

    void Update()
    {
        // 移動
        if (!pushedFlag){
            creditChange();
        }
        else {
            delta += Time.deltaTime; 
            if (delta > inputWaitSpan){
                delta = 0;
                pushedFlag = false;
            }
        }
    }

}
