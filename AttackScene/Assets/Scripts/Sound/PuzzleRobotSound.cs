using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRobotSound : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static PuzzleRobotSound soundManagerInstance;
    public AudioSource audioSource;


    [SerializeField]
    private AudioClip puzzleRobotAttack;

    private void Awake()
    {
        soundManagerInstance = this;
    }



    public void puzzleRobotAttackAudioPlay()
    {
       audioSource.clip = puzzleRobotAttack;
        audioSource.Play();
    }

}
