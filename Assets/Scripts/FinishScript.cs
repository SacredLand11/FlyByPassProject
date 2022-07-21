using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    public GameObject faderPrefab;
    public Text moneyText;
    public int money = 0;
    private void Start()
    {
        moneyText = GameObject.Find("MoneyTextCanvas(Clone)").gameObject.transform.GetChild(0).GetComponent<Text>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            Instantiate(faderPrefab);
            money += 100;
        }
    }
}
