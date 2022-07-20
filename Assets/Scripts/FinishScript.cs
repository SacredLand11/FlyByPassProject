using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishScript : MonoBehaviour
{
    public GameObject faderPrefab;
    PlayerMovement playerMovement;
    public Text moneyText;
    public int money = 0;
    private void Start()
    {
        moneyText = GameObject.Find("MoneyTextCanvas(Clone)").gameObject.transform.GetChild(0).GetComponent<Text>();
        playerMovement = GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            Instantiate(faderPrefab);
            money += 100;
        }
    }
    private void Update()
    {
        if (playerMovement.moneycollect)
        {
            moneyText.text = money.ToString();
        }
        
    }
}
