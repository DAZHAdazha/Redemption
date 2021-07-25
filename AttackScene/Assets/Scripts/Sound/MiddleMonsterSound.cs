using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleMonsterSound : MonoBehaviour
{
    //����ģʽ
    //���ɾ�̬�������ʵ�����˷���Դ
    public static MiddleMonsterSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip MiddleMonsterAttack1, MiddleMonsterAttack2;

    private void Awake()
    {
        soundManagerInstance = this;
    } 



    public void MiddleMonsterAttack1AudioPlay()
    {
        audioSource.clip = MiddleMonsterAttack1;
        audioSource.Play();
    }

    public void MiddleMonsterAttack2AudioPlay()
    {
        audioSource.clip = MiddleMonsterAttack2;
        audioSource.Play();
    }





}
