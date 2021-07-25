using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonsterSound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static SmallMonsterSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip SmallMonsterAttack;

    private void Awake()
    {
        soundManagerInstance = this;
    } 



    public void SmallMonsterAttackAudioPlay()
    {
        audioSource.clip = SmallMonsterAttack;
        audioSource.Play();
    }






}
