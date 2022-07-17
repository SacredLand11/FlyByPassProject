using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject RoadPrefab;
    public GameObject BrickPrefab;
    public GameObject player;
    void Awake()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        for (int i = 0; i < 20; i++)
        {
            Vector3 instantiatePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + i * 4);
            Instantiate(RoadPrefab, instantiatePosition, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        }
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-1, 2)*3, 0, 7.5f * i);
            Instantiate(BrickPrefab, randomSpawnPosition, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        }
    }
}
