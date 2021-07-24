using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare1Sound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static Nightmare1Sound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip Nightmare1Attack, Nightmare1Vanish;

    private void Awake()
    {
        soundManagerInstance = this;
    }



    public void Nightmare1AttackAudioPlay()
    {
        audioSource.clip = Nightmare1Attack;
        audioSource.Play();
    }

    public void Nightmare1VanishAudioPlay()
    {
        audioSource.clip = Nightmare1Vanish;
        audioSource.Play();
    }




}
