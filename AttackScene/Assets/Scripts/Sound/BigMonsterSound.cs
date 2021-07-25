using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMonsterSound : MonoBehaviour
{
    //����ģʽ
    //���ɾ�̬�������ʵ�����˷���Դ
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
