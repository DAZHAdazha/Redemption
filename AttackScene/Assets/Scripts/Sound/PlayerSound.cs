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
    private AudioClip batHit, puzzlebotHit, skeletonHit, firewarmHit, smallStoneMonsterHit, middleStoneMonsterHit, bigStoneMonsterHit,
        nightmare1Hit, nightmare2Hit;

    private void Awake()
    {
        soundManagerInstance = this;
    }
}
