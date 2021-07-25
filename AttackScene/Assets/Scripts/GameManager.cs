using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    private PlayerController playerController;
    private GameObject canvas;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        //DontDestroyOnLoad(this);
    }
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        if(GameObject.Find("Player"))
            playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
     public void Menu()
    {
        //SceneManager.LoadScene(0);
        canvas.GetComponent<MainMenu>().LoadLevel(0);
        Save();
    }
    public void GameStart()
    {
        //SceneManager.LoadScene(1
        canvas.GetComponent<MainMenu>().LoadLevel(1);
        Save();
    }
    public void LevelFear()
    {
        //SceneManager.LoadScene(2);
        canvas.GetComponent<MainMenu>().LoadLevel(2);
        Save();
    }
    public void LevelAngry()
    {
        //SceneManager.LoadScene(3);
        canvas.GetComponent<MainMenu>().LoadLevel(3);
        Save();
    }
    public void LevelPuzzle()
    {
        //SceneManager.LoadScene(4);
        canvas.GetComponent<MainMenu>().LoadLevel(4);
        Save();
    }
    public void LevelSorrow()
    {
        //SceneManager.LoadScene(5);
        canvas.GetComponent<MainMenu>().LoadLevel(5);
        Save();
    }
    public void LevelNightmare()
    {
        //SceneManager.LoadScene(6);
        canvas.GetComponent<MainMenu>().LoadLevel(6);
        Save();
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Save()
    {
        if (playerController)
        {
            GameSaver.healthMax = playerController.health;
            GameSaver.manaMax = playerController.mana;
            GameSaver.attackLock = playerController.attackLock;
            GameSaver.duckLock = playerController.duckLock;
            GameSaver.shadowLock = playerController.shadowLock;
            GameSaver.bonusLock = playerController.bonusLock;
            GameSaver.defenseLock = playerController.defenseLock;
            GameSaver.coinNum = CoinUI.coinNum;
        }

    }

    public void buySmallHealth()
    {
        GameSaver.healthMax += 5;
        //lock the item;
    }

    public void buyBigHealth()
    {
        GameSaver.healthMax += 10;
        //lock the item;
    }

    public void buySmallMana()
    {
        GameSaver.manaMax += 5;
        //lock the item;
    }

    public void buyBigMana()
    {
        GameSaver.manaMax += 10;
        //lock the item;
    }

    public void setAttack()
    {
        GameSaver.attackLock = true;
    }

    public void setDuck()
    {
        GameSaver.duckLock = true;
    }

    public void setBonus()
    {
        GameSaver.bonusLock = true;
    }

    public void setDefense()
    {
        GameSaver.defenseLock = true;
    }

    public void setShadow()
    {
        GameSaver.shadowLock = true;
    }

    public void godMode()
    {
        setAttack();
        setBonus();
        setDefense();
        setDuck();
        setShadow();
        GameSaver.healthMax = 1000;
        GameSaver.manaMax = 1000;
        GameSaver.unLockLevel = 5;
    }

}
