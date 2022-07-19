using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    public GameObject faderPrefab;
    public int money = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            Instantiate(faderPrefab);
            money += 100;
        }
    }
}
