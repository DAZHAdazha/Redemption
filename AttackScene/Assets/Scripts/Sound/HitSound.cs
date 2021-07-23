using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static HitSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip batHit,puzzlebotHit,skeletonHit,firewarmHit, smallStoneMonsterHit, middleStoneMonsterHit, bigStoneMonsterHit,
        nightmare1Hit,nightmare2Hit;

    private void Awake()
    {
        soundManagerInstance = this;
    }

    public void batHitAudio()
    {
        audioSource.clip = batHit;
        audioSource.Play();
    }

    public void puzzlebotAudio()
    {
        audioSource.clip = puzzlebotHit;
        audioSource.Play();
    }

    public void skeletontAudio()
    {
        audioSource.clip = skeletonHit;
        audioSource.Play();
    }

    public void firewarmAudio()
    {
        audioSource.clip = firewarmHit;
        audioSource.Play();
    }

    public void smallStoneMonsterAudio()
    {
        audioSource.clip = smallStoneMonsterHit;
        audioSource.Play();
    }

    public void middleStoneMonsterAudio()
    {
        audioSource.clip = middleStoneMonsterHit;
        audioSource.Play();
    }
    public void bigStoneMonsterHitAudio()
    {
        audioSource.clip = bigStoneMonsterHit;
        audioSource.Play();
    }
    public void nightmare1HitAudio()
    {
        audioSource.clip = nightmare1Hit;
        audioSource.Play();
    }
    public void nightmare2HitAudio()
    {
        audioSource.clip = nightmare2Hit;
        audioSource.Play();
    }

}
