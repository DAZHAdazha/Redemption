using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Plot : MonoBehaviour
{
    private PlayerInputActions controls;
    public GameObject Button;
    public GameObject talkUI;
    public GameObject actionButton;

    [Header("UI����")]
    public Text textLabel;
    public Image faceImage;

    [Header("�ļ�")]
    public TextAsset textFile;

    public int index = 0;
    public float textSpeed;
    private bool textFinished = true;
    private bool cancelTyping = false;

    [Header("�Ի�ͷ��")]
    public Sprite face1;
    public Sprite face2;


    private bool outside;
    List<string> textList = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Button.SetActive(true);
            outside = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            Button.SetActive(false);
            talkUI.SetActive(false);
            actionButton.SetActive(false);
            
            textLabel.text = "";
            outside = true;
        }
        
    }

    

    private void Awake()
    {
        getTextFromFile(textFile);
        controls = new PlayerInputActions();
        controls.GamePlay.EnterDoor.started += ctx => activateDialogue();

    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }


    public void activateDialogue()
    {
        if (outside)
        {
            index = 0;
        }
        if (Button.activeSelf)
        {
            talkUI.SetActive(true);

            if(index == (textList.Count-1))
                actionButton.SetActive(true);

            Time.timeScale = 0f;
            GameObject.Find("Player").GetComponent<PlayerController>().isStop = true;
            
            if (index == textList.Count)
            {
                talkUI.SetActive(false);
                Time.timeScale = 1f;
                GameObject.Find("Player").GetComponent<PlayerController>().isStop = false;
                index = 0;
                return;
            }
            else
            {
                switch (textList[index])
                {
                    case "A\r":
                        faceImage.sprite = face1;
                        index++;
                        break;
                    case "B\r":
                        faceImage.sprite = face2;
                        index++;
                        break;
                }
                Debug.Log(textList[index]);   
                textLabel.text = textList[index++];
                return;
               
            }
            
        }
        
    }

    public void deactiveDialogue()
    {
        talkUI.SetActive(false);
    }

    void getTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }
}
