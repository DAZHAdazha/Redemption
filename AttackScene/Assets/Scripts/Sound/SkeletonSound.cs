using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonSound : MonoBehaviour
{
    //����ģʽ
    //���ɾ�̬�������ʵ�����˷���Դ
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
