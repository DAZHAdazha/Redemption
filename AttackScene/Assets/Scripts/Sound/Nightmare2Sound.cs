using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nightmare2Sound : MonoBehaviour
{
    //����ģʽ
    //���ɾ�̬�������ʵ�����˷���Դ
    public static Nightmare2Sound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip Nightmare2Attack;

    private void Awake()
    {
        soundManagerInstance = this;
    } 



    public void Nightmare2AttackAudioPlay()
    {
        audioSource.clip = Nightmare2Attack;
        audioSource.Play();
    }





}
