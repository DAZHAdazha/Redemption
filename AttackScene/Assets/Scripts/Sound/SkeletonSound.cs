using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static SkeletonSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip SkeletonAttack;

    private void Awake()
    {
        soundManagerInstance = this;
    } 



    public void SkeletonAttackAudioPlay()
    {
        audioSource.clip = SkeletonAttack;
        audioSource.Play();
    }






}
