using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusCubeSpawner : MonoBehaviour
{
    public GameObject bonusCube;
    public List<GameObject> bonusList = new List<GameObject>();
    void Start()
    {
        //Create the bonus cubes with random colors
        for (int i = 0; i < 9; i++)
        {
            bonusList.Add(Instantiate(bonusCube, new Vector3(0, -10, 8 * i + 267), Quaternion.identity));
            bonusList[i].transform.SetParent(this.transform);
            bonusList[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = (i + 1) + "X";
            bonusList[i].GetComponent<BonusCubeScript>().multiply = i + 1;
            bonusList[i].GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(1, 100) * 0.01f, Random.Range(1, 100) * 0.01f, Random.Range(1, 100) * 0.01f));//Color(1 -0.08f*i, 0.3f + 0.152f * i, 0.7f - 0.07f * i, 1));
        }
    }
}
