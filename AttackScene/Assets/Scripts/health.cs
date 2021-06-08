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
    private Image healthBar;
    public GameObject enemy;
    private Vector2 currentPosition;
    public GameObject healthBarObject;
    void Start()
    {
        healthBar = GetComponent<Image>();
        healthMax = enemy.GetComponent<FSM>().parameter.health;
        healthCurrent = healthMax;
        updateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = new Vector2(enemy.transform.position.x + 0.2f,enemy.transform.position.y + 1.3f);
        transform.position = currentPosition;
        healthText.transform.position = currentPosition;
        healthBarObject.transform.position = currentPosition;
    }

    public void updateHealthBar(){
        healthBar.fillAmount = (float)healthCurrent / (float)healthMax;
        healthText.text = healthCurrent.ToString() + "/" + healthMax.ToString();
    }

    public void callUpdateHealth(){
        healthCurrent = enemy.GetComponent<FSM>().parameter.health;
        updateHealthBar();
    }
}
