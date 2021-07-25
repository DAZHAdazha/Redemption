using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonsterSound : MonoBehaviour
{
    //����ģʽ
    //���ɾ�̬�������ʵ�����˷���Դ
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
