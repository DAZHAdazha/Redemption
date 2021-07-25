using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class SkipCG : BasicPlayableBehaviour
{
    private PlayerInputActions controls;

    private void Awake()
    {
        controls = new PlayerInputActions();
        controls.GamePlay.EnterDoor.started += ctx => skipCG();
    }

    //public virtual void ProcessFrame(Playable playable, FrameData info, object playerData)
    //{

    //}

    //public virtual void OnPlayStateChanged(FrameData info, PlayState newState)
    //{
        
    //}

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    private void skipCG()
    {
        if(SceneManager.GetActiveScene().name=="FinalCG")
            SceneManager.LoadScene(0);
        else
        {
            SceneManager.LoadScene(6);
        }
    }


}
