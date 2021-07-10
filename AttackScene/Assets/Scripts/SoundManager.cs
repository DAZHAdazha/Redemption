using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //单例模式
    //生成静态类避免多次实例化浪费资源
    public static SoundManager soundManagerInstance;
    public AudioSource audioSource;
    
    [SerializeField]
    private AudioClip pickCoin;

    private void Awake() {
        soundManagerInstance = this;
    }

    public void pickCoinAudio(){
        audioSource.clip = pickCoin;
        audioSource.Play();
    }
}
