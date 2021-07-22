using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    private PlayerController playerController;
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
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
    }
     public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }
    public void LevelFear()
    {
        SceneManager.LoadScene(2);
    }
    public void LevelAngry()
    {
        SceneManager.LoadScene(3);
    }
    public void LevelPuzzle()
    {
        SceneManager.LoadScene(4);
    }
    public void LevelSorrow()
    {
        SceneManager.LoadScene(5);
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Save()
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



//application.Quit
}
