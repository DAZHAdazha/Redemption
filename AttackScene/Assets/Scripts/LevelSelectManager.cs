using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    public GameObject levelSelectPanel;
    public Button btfear;
    public Button btanger;
    public Button btpuzzle;
    public Button btsorrow;
    public Button btboss;
    public Image lock1,lock2,lock3,lock4,lock5;
    public Text name1,name2,name3,name4,name5;

    private int unlockedLevelIndex;

    void Start()
    {
        setUnlock();
    }

    public void setUnlock()
    {
        //unlockedLevelIndex = PlayerPrefs.GetInt("unlockedLevel");
        unlockedLevelIndex = GameSaver.unLockLevel;
        //Debug.Log(unlockedLevelIndex);
        Button[] levelSelecButtons = { btfear, btanger, btpuzzle, btsorrow, btboss };
        Image[] locked = { lock1, lock2, lock3, lock4, lock5 };
        Text[] names = { name1, name2, name3, name4, name5 };

        for (int i = 0; i < 5; i++)
        {
            levelSelecButtons[i].interactable = false;
            names[i].gameObject.SetActive(false);
            locked[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < unlockedLevelIndex; i++)
        {
            levelSelecButtons[i].interactable = true;
            names[i].gameObject.SetActive(true);
            locked[i].gameObject.SetActive(false);

        }
    }

}
