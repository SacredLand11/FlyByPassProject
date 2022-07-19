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
        for (int i = 0; i < 9; i++)
        {
            bonusList.Add(Instantiate(bonusCube, new Vector3(0, -10, 8 * i + 131), Quaternion.identity));
            bonusList[i].transform.SetParent(this.transform);
            bonusList[i].gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<Text>().text = (i + 1) + "X";
            bonusList[i].GetComponent<BonusCubeScript>().multiply = i + 1;
        }
    }
}
