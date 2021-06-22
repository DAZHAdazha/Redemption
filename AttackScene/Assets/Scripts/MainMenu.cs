using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    // public void PlayGame(){
    //     SceneManager.LoadScene(1);
    // }

    public void QuitGame(){
        Application.Quit();
    }

    public void LoadLevel(int sceneIndex){
        StartCoroutine(AsyncLoadLevel(sceneIndex));
    }

    IEnumerator AsyncLoadLevel(int sceneIndex){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        loadingScreen.SetActive(true);
        while(!operation.isDone){
            float progress = operation.progress / 0.9f;// operation.progress的值为0-0.9之间 所以除0.9
            slider.value = progress;
            progressText.text = Mathf.FloorToInt(progress * 100f).ToString() + "%";
            yield return null;
        }
        
    }
}
