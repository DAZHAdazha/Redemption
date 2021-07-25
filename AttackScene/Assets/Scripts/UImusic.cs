using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImusic: MonoBehaviour
 {
    public Sprite pause ;
    private Sprite play;

    private bool ischange = false;
    private AudioSource music;
    // Use this for initialization
    void Start () {

        music = GameObject.Find("BGM").GetComponent<AudioSource>();
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
        play = transform.GetComponent<Image>().sprite;

    }
    void OnClick()
    {
       
        ischange = !ischange;
        if (ischange)
        {
            ///更改按钮图片
            transform.GetComponent<Image>().sprite = pause;
            music.Pause();
        }
        else
        {
            ///还原按钮图片
            transform.GetComponent<Image>().sprite = play;
            music.Play();
        }
    }
 }