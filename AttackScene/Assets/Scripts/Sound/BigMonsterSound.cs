using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterSound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static BigMonsterSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip BigMonsterAttack1, BigMonsterAttack2;

    private void Awake()
    {
        soundManagerInstance = this;
    } 



    public void BigMonsterAttack1AudioPlay()
    {
        audioSource.clip = BigMonsterAttack1;
        audioSource.Play();
    }

    public void BigMonsterAttack2AudioPlay()
    {
        audioSource.clip = BigMonsterAttack2;
        audioSource.Play();
    }





}
