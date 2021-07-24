using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //����ģʽ
    //���ɾ�̬�������ʵ�����˷���Դ
    public static PlayerSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip hurtAudio,showUpAudio;

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

    public void showUpPlay()
    {
       audioSource.clip = showUpAudio;
       audioSource.Play();
    }

}
