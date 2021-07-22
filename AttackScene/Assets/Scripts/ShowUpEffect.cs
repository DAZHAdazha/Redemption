using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUpEffect : MonoBehaviour
{
    private Transform playerTransform;
    private PlayerController playerController;

    private PlayerInputActions controls;
    private Vector2 move;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").gameObject.transform;
        playerController = playerTransform.gameObject.GetComponent<PlayerController>();
    }

    public void hidePlayer()
    {
        //move = playerController.move;
        playerTransform.gameObject.SetActive(false);
        
    }

    public void destroyEffect()
    {
        
        playerTransform.gameObject.SetActive(true);
        playerController.move = move;
        Destroy(gameObject);
    }


    private void Awake()
    {

        //Ö§³ÖÊÖ±ú
        controls = new PlayerInputActions();

        //controls.GamePlay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.started += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Move.canceled += ctx => move = Vector2.zero;
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }

}
