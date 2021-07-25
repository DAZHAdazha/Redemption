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
    private AudioClip Nightmare2Attack, Nightmare2Sweep, Nightmare2Defense;

    private void Awake()
    {
        soundManagerInstance = this;
    } 



    public void Nightmare2AttackAudioPlay()
    {
        audioSource.clip = Nightmare2Attack;
        audioSource.Play();
    }

    public void Nightmare2SweepAudioPlay()
    {
        audioSource.clip = Nightmare2Sweep;
        audioSource.Play();
    }

    public void Nightmare2DefenseAudioPlay()
    {
        audioSource.clip = Nightmare2Defense;
        audioSource.Play();
    }





}
