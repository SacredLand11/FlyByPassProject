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
    float yaw = 0.0f;
    bool isFly = false;
    bool isre_Fly = true;
    bool falling = false;
    bool courotineBool = false;
    Vector3 flyVec;


    private void Start()
    {
        brickScore = 0;
        a = 0; // List Number
        GetComponent<Animator>().SetBool("Fly", false);
        flyVec = new Vector3(0, 2f, 0);
    }
    private void Update()
    {
        brickText.text = brickScore.ToString();
        Movement();
    }
    private void LateUpdate()
    {     
        if (isFly)
        {
            if (isre_Fly)
            {
                this.transform.Translate(flyVec * Time.deltaTime * 1.5f);
            }
            if (Input.GetMouseButton(0))
            {
                if (brickScore > 0)
                {
                    if (!courotineBool)
                    {
                        StartCoroutine(decreaseTheBricks(.15f));
                        courotineBool = true;
                    }
                    falling = false;
                    this.transform.Translate(new Vector3(0, 0, 0));
                }
                if (brickScore == 0)
                {
                    courotineBool = false;
                    isre_Fly = false;
                    falling = true;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                isre_Fly = false;
                falling = true;
                courotineBool = false;
            }
            if (this.transform.position.y > 2)
            {
                this.transform.Translate(new Vector3(0, 0, 0));
                isre_Fly = false;
                falling = true;
            }
            if (!isre_Fly && falling)
            {
                this.transform.Translate(-flyVec * Time.deltaTime);
            }
            if(this.transform.position.y == 0)
            {
                isFly = false;
            }
        }
    }

    IEnumerator decreaseTheBricks(float i)
    {
        while (brickScore > 0)
        {
            yield return new WaitForSeconds(i);
            brickScore--;
            if(falling)
            {
                break;
            }
        }
    }
    public void StartGameKey()
    {
        GetComponent<Animator>().SetTrigger("StartGame");
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {
            yaw += Input.GetTouch(0).deltaPosition.x * 10 * Time.deltaTime;
            yaw = Mathf.Clamp(yaw, -80, 80);
            this.transform.eulerAngles = new Vector3(0, yaw, 0);
        }
        /*float horizontalpath = Input.GetAxis("Horizontal") * 6.5f * Time.deltaTime;
        this.transform.Translate(horizontalpath, 0, 0);*/
    }
    public void Dance()
    {
        GetComponent<Animator>().SetTrigger("BonusTrig");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bricks")
        {
            brickScore += 3;
            a += 1;
            Destroy(other.gameObject);
            //Vector3 brickPos = new Vector3(transform.position.x, transform.position.y + 0.25f + (a-5) * 0.015f, transform.position.z -0.025f * (a-5));
            Quaternion angle = this.transform.rotation;
            //Object.Instantiate(smallBrickPrefab, brickPos, angle, this.transform);

            brickList.Add(Instantiate(smallBrickPrefab, transform.position, angle));
            brickList[a - 1].transform.SetParent(this.transform);
            if(a == 1)
            {
                brickList[0].transform.rotation = this.transform.rotation;
                brickList[0].transform.position = this.transform.position;
            }

            if (a >= 2)
            {
                brickList[a - 1].transform.rotation = this.transform.rotation;
                brickList[a - 1].transform.position = this.transform.position;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Plane")
        {
            GetComponent<Animator>().SetBool("Fly", false);
            isFly = false;
            isre_Fly = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Plane")
        {
            GetComponent<Animator>().SetBool("Fly", true);
            isFly = true;
        }
    }
}
