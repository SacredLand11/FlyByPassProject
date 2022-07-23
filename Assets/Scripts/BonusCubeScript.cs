using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusCubeScript : MonoBehaviour
{
    //Scripts
    PlayerMovement player;
    BotMovementScript bot;
    BotMovementScript bot2;
    [Header("Integers")]
    public int multiply;
    public int claimButtonNumber = 0;
    [Header("GameObjects")]
    public GameObject claimButton;
    [Header("Bools")]
    public bool won;
    private void Start()
    {
        won = false;
        player = GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>();
        bot = GameObject.Find("Bot(Clone)").GetComponent<BotMovementScript>();
        bot2 = GameObject.Find("Bot2(Clone)").GetComponent<BotMovementScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Dance & Money Conditions
        if (other.gameObject.tag == "Player")
        {
            player.Dance();
            if (!won)
            {
                GameObject.FindGameObjectWithTag("LevelRoad").GetComponent<FinishScript>().money *= multiply;
                won = true;
            }
            if(claimButtonNumber == 0)
            {
                Instantiate(claimButton);
                claimButtonNumber++;
            }
              
        }
        if (other.gameObject.tag == "Bot")
        {
            bot.Dance();
            bot2.Dance();
        }

    }
}
