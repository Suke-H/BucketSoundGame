using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditController : MonoBehaviour
{
    [SerializeField] float inputWaitSpan;
    [SerializeField] int currentPos;

    private float delta;
    bool pushedFlag = false;

    private void creditChange(){

        // 左に移動
        if (Input.GetKey (KeyCode.LeftArrow) | Input.GetKey (KeyCode.A)) { 
            pushedFlag = true;
            if (currentPos == 1){ SceneManager.LoadScene("Credit0"); }
            
        }
        // 右に移動
        if (Input.GetKey (KeyCode.RightArrow) | Input.GetKey (KeyCode.D)) { 
            pushedFlag = true;
            if (currentPos == 0){ SceneManager.LoadScene("Credit1"); }
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
