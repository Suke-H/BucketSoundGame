using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorController : MonoBehaviour
{

    [SerializeField] GameObject[] cursorPosList;
    [SerializeField] float inputWaitSpan;

    private int currentPos = 0;

    private float delta;
    bool pushedFlag = false;

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
            if (currentPos == 0){ SceneManager.LoadScene("SoundGame"); }
            else { SceneManager.LoadScene("Credit0"); }
        }
    }

    void Update()
    {
        // 移動
        if (!pushedFlag){
            cursorInput();
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
