using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCubeScript : MonoBehaviour
{
    PlayerMovement player;
    public int multiply;
    public GameObject claimButton;
    public bool won;
    private void Start()
    {
        won = false;
        player = GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>();
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
    }
}
