using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defense : MonoBehaviour
{
    public void resolveDefense(){
        gameObject.SetActive(false);
        gameObject.transform.parent.GetComponent<PlayerController>().unableDefense();
    }
}
