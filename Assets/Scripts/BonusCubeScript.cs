using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCubeScript : MonoBehaviour
{
    PlayerMovement player;
    public int multiply;
    private void Start()
    {
        player = GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Dance();
            GameObject.Find("Level1Path(Clone)/FinishRoad").GetComponent<FinishScript>().money *= multiply;
        }
    }
}
