using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class health : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public static int healthCurrent;
    public static int healthMax;
    public GameObject healthBarObject;
    public GameObject enemy;



    private Vector2 currentPosition;
    private Image healthBar;
    private int type;
    
    void Start()
    {
        healthBar = GetComponent<Image>();
        if (enemy.GetComponent<FSM>())
        {
            healthMax = enemy.GetComponent<FSM>().parameter.health;
            type = 0;
        }
        else if(enemy.GetComponent<FSM_PuzzleRobot>())
        {
            healthMax = enemy.GetComponent<FSM_PuzzleRobot>().parameter.health;
            type = 1;
        }
        else if (enemy.GetComponent<fireWormAction>())
        {
            healthMax = enemy.GetComponent<fireWormAction>().maxHealth;
            type = 2;
        }
        else if (enemy.GetComponent<FSM_Nightmare1>())
        {
            healthMax = enemy.GetComponent<FSM_Nightmare1>().parameter.health;
            type = 3;
        }
        
        healthCurrent = healthMax;
        updateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        if (type != 3)
        {
            currentPosition = new Vector2(enemy.transform.position.x + 0.2f,enemy.transform.position.y + 1.3f);
        }
        else
        {
            currentPosition = new Vector2(enemy.transform.position.x + 0.2f, enemy.transform.position.y + 2.3f);
        }
        
        transform.position = currentPosition;
        healthText.transform.position = currentPosition;
        healthBarObject.transform.position = currentPosition;
    }

    public void updateHealthBar(){
        healthBar.fillAmount = (float)healthCurrent / (float)healthMax;
        healthText.text = healthCurrent.ToString() + "/" + healthMax.ToString();
    }

    public void callUpdateHealth(){
        if (type == 0)
        {
            healthCurrent = enemy.GetComponent<FSM>().parameter.health;
        }else if(type == 1)
        {
            healthCurrent = enemy.GetComponent<FSM_PuzzleRobot>().parameter.health;
        }else if(type == 2)
        {
            healthCurrent = (int)enemy.GetComponent<fireWormAction>().currentHealth;
        }
        else if (type == 3)
        {
            healthCurrent = enemy.GetComponent<FSM_Nightmare1>().parameter.health;
        }

        updateHealthBar();
    }
}
