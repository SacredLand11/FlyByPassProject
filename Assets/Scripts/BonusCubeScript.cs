using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusCubeScript : MonoBehaviour
{
    PlayerMovement player;
    BotMovementScript bot;
    BotMovementScript bot2;
    public int multiply;
    public GameObject claimButton;
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
        if (other.gameObject.tag == "Player")
        {
            player.Dance();
            if (!won)
            {
                GameObject.Find("Level1Path(Clone)/FinishRoad").GetComponent<FinishScript>().money *= multiply;
                won = true;
            }
            Instantiate(claimButton);  
        }
        if (other.gameObject.tag == "Bot")
        {
            bot.Dance();
            bot2.Dance();
        }

    }
}
