using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleArea : MonoBehaviour
{
    private PlayerController playerController;
    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            Invoke("setPuzzleTrue", 0.5f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            Invoke("setPuzzleFalse", 0.5f);
        }
    }

    private void setPuzzleTrue()
    {
        playerController.isPuzzled = true;
        playerController.status.SetActive(true);
    }

    private void setPuzzleFalse()
    {
        playerController.isPuzzled = false;
        playerController.status.SetActive(false);
    }

}
