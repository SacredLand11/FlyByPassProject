using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform chrPos;
    Vector3 distance;
    void Start()
    {
        chrPos = GameObject.Find("Character(Clone)").transform;
        distance = transform.position - chrPos.position;
    }
    private void Update()
    {
        transform.position = chrPos.position + distance;
    }
}
