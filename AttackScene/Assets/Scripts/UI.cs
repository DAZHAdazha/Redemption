using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI : MonoBehaviour
{
    public GameObject manual;
    public void showManual(){
        manual.SetActive(true);
        Time.timeScale = 0f;
        GameObject.Find("Player").GetComponent<PlayerController>().isStop = true;
    }

    public void hideManual(){
        manual.SetActive(false);
        Time.timeScale = 1f;
        GameObject.Find("Player").GetComponent<PlayerController>().isStop = false;
    }

}
