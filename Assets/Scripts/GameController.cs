using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム中にEscでタイトルへ遷移する用のスクリプト
/// </summary>
public class GameController : MonoBehaviour
{
    void Update()
    {
        // Escでタイトルへ
        if (Input.GetKey (KeyCode.Escape)) { 
            SceneManager.LoadScene("Title");
        }
    }
    
}
