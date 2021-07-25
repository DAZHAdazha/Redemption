using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static PlayerSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip hurtAudio;

    private void Awake()
    {
        soundManagerInstance = this;
    }



    public void hurtAudioPlay()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = hurtAudio;
            audioSource.Play();
        }
    }


}
