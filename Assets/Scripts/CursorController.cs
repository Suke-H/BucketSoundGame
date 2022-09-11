using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject[] cursorPosList;
    [SerializeField] string[] SceneList;
    [SerializeField] float inputWaitSpan;

    private int currentPos = 0;

    private float delta_1;
    private float delta_2;
    bool pushedFlag = false;
    bool startFlag = false;

    private void cursorInput(){
        int nextPos = currentPos;

        // 上に移動
        if (Input.GetKey (KeyCode.UpArrow) | Input.GetKey (KeyCode.W)) { 
            nextPos = 0;
            pushedFlag = true;
        }
        // 下に移動
        if (Input.GetKey (KeyCode.DownArrow) | Input.GetKey (KeyCode.S)) { 
            nextPos = 1;
            pushedFlag = true;
        }

        // 移動
        currentPos = nextPos;
        transform.position = cursorPosList[currentPos].transform.position;

        // シーン遷移
        if (Input.GetKey (KeyCode.Return)) { 
            SceneManager.LoadScene(SceneList[currentPos]);
        }
    }

    void Update()
    {
        delta_1 += Time.deltaTime; 
        if (delta_1 > 1.0f){
            delta_1 = 0;
            startFlag = true;
        }

        // 移動
        if (startFlag){
            if (!pushedFlag){
                cursorInput();
            }

            else {
                delta_2 += Time.deltaTime; 
                if (delta_2 > inputWaitSpan){
                    delta_2 = 0;
                    pushedFlag = false;
                }
            }
        }
    }
}
