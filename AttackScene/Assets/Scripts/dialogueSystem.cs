using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class dialogueSystem : MonoBehaviour
{
    private PlayerInputActions controls;


    [Header("UI参数")]
    public Text textLabel;
    public Image faceImage;

    [Header("文件")]
    public TextAsset textFile;

    public int index = 0;
    public float textSpeed;
    private bool textFinished = true;
    private bool cancelTyping = false;

    [Header("对话头像")]
    public Sprite face1;
    public Sprite face2;

    List<string> textList = new List<string>();

    // Start is called before the first frame update
    public void Awake()
    {
        getTextFromFile(textFile);
        controls = new PlayerInputActions();
        controls.GamePlay.EnterDoor.started += ctx => AfterPressing();
    }

    
    void getTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');
        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }

    public void Update()
    {
        
    }

    /*IEnumerator setTextUI()
    {
        textFinished = false;
        textLabel.text = "";
        switch (textList[index])
        {
            case "A":
                faceImage.sprite = face1;
                index++;
                break;
            case "B":
                faceImage.sprite = face2;
                index++;
                break;
        }

        int i = 0;
        while(!cancelTyping && i < textList[index].Length)
        {
            textLabel.text += textList[index][i];
            i++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];
        cancelTyping = !cancelTyping;
        index++;
        textFinished = true;
    }*/

    private void AfterPressing()
    {
        if(index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
        else
        {
            textLabel.text = textList[index++];
            faceImage.sprite = face1;
        }
        //if(textFinished && !cancelTyping)
        //{
        //    StartCoroutine(setTextUI());
        //}
        //else if (!textFinished)
        //{
        //    cancelTyping = !cancelTyping;
        //}
    }
}
