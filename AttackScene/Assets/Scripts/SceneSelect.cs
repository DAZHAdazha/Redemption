using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect: MonoBehaviour
{
/** scene in build:
    0.start
    1.levelselector
    2.patrol
    3.dialog
    4.shadow
    5.tutorial
    6.theme  **/
    // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   public void patrol()
   {
       SceneManager.LoadScene(2);
   }
    public void dialog()
   {
       SceneManager.LoadScene(3);
   }
   public void shadow()
   {
       SceneManager.LoadScene(4);
   }
   public void tutorial()
   {
       SceneManager.LoadScene(5);
   }
   public void theme()
   {
       SceneManager.LoadScene(6);
   }

   public void levelselect()
   {
       SceneManager.LoadScene(1);
   }
     public void start()
   {
       SceneManager.LoadScene(0);
   }



}
