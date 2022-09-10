using UnityEngine;
using System.Collections;

public class Result : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] double FadeOutStart;
    [SerializeField] double FadeOutEnd;

    double FadeSpan;

    bool IsFadeOut = true;
    double FadeDeltaTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        FadeSpan = FadeOutEnd - FadeOutStart;
    }

    void Update()
    {
        if (IsFadeOut){
            FadeDeltaTime += Time.deltaTime;

            if (FadeDeltaTime >= FadeOutStart){
                audioSource.volume = (float)(1.0 - (FadeDeltaTime - FadeOutStart) / FadeSpan);
            }

            if (FadeDeltaTime > FadeOutEnd){
                IsFadeOut = false;
            }
        }


    } 
}