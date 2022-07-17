using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject smallBrickPrefab;
    [Header("Texts")]
    public Text brickText;
    [Header("List")]
    [SerializeField] public List<GameObject> brickList = new List<GameObject>();

    int brickScore;
    int a;
    private void Start()
    {
        brickScore = 0;
        a = 0;
    }
    private void Update()
    {
        brickText.text = brickScore.ToString();
        Movement();
        StartGameKey();
    }

    private void StartGameKey()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("StartGame");
        }
    }

    private void Movement()
    {
        float horizontalpath = Input.GetAxis("Horizontal") * 6.5f * Time.deltaTime;
        this.transform.Translate(horizontalpath, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bricks")
        {
            brickScore += 3;
            a += 1;
            Destroy(other.gameObject);
            Vector3 brickPos = new Vector3(0, 0.25f + (a) * 0.015f, -0.025f * (a));
            brickList.Add(Instantiate(smallBrickPrefab, brickPos, Quaternion.identity));
            brickList[a - 1].transform.SetParent(this.transform);
        }
    }
}
