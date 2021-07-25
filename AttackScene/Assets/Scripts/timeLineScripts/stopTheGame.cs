using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class stopTheGame : BasicPlayableBehaviour
{
    public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (SceneManager.GetActiveScene().name == "FinalCG")
            SceneManager.LoadScene(0);
        else if(SceneManager.GetActiveScene().name == "bossCG")
        {
            SceneManager.LoadScene(7);
        }
    }

    public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    {

    }

}
