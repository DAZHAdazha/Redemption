using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetting : MonoBehaviour
{
    public GameManager gameManager;


    public void setNormal()
    {
        GameSaver.difficulty = 0;
        GameSaver.healthMax = 15;
        GameSaver.manaMax = 10;
    }

    public void setHard()
    {
        GameSaver.difficulty = 1;
        GameSaver.healthMax = 15;
        GameSaver.manaMax = 10;
    }

    public void godMode()
    {
        gameManager.godMode();
    }

}
