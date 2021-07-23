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
    private AudioClip batHit, puzzlebotHit, skeletonHit, firewarmHit, smallStoneMonsterHit, middleStoneMonsterHit, bigStoneMonsterHit,
        nightmare1Hit, nightmare2Hit;

    private void Awake()
    {
        soundManagerInstance = this;
    }
}
