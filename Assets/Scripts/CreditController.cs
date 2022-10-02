using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// クレジット画面制御
/// </summary>
public class CreditController : MonoBehaviour
{
    [SerializeField] float inputWaitSpan; // 移動キー入力後に（連打防止で）一定時間入力禁止するスパン
    [SerializeField] int currentPos; // 現在位置（ページ0, 1）

    private float delta;
    bool pushedFlag = false; // 移動キー入力禁止中かどうか

    /// <summary>
    /// クレジット画面のページ移動（シーン遷移）
    /// </summary>
    private void creditChange(){
        // 左に移動
        if (Input.GetKey (KeyCode.LeftArrow) | Input.GetKey (KeyCode.A)) { 
            pushedFlag = true; // 入力禁止フラグ
            if (currentPos == 1){ // 現在ページ1ならページ0へ移動
                SceneManager.LoadScene("Credit0"); 
            }
            
        }
        // 右に移動
        if (Input.GetKey (KeyCode.RightArrow) | Input.GetKey (KeyCode.D)) { 
            pushedFlag = true;
            if (currentPos == 0){ // 現在ページ0ならページ1へ移動
                SceneManager.LoadScene("Credit1"); 
            }
        }

        // Escでタイトルへ移動
        if (Input.GetKey (KeyCode.Escape)) { 
            SceneManager.LoadScene("Title");
        }
    }

    void Update()
    {
        // 移動（連打防止のため移動キー入力禁止待ちがある）
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