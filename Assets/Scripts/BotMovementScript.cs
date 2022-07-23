using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotMovementScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject smallBrickPrefab; //Collected Bricks
    public GameObject BrickPrefab; //Ground Bricks
    [Header("Texts")]
    public Text brickText;

    //Brick Variables
    int brickScore; //To determine total collected brick number
    int a; //Brick object list number

    //Find the closest brick Variables
    GameObject[] multiplebricks;
    public Transform closestBrick;
    public bool brickContact;

    //Flying variables
    bool isFly = false;
    bool isre_Fly = true;
    bool falling = false;
    bool courotineBool = false;
    bool botfalling = false;
    Vector3 flyVec;

    //Start variables
    bool start = false;
    public bool open;
    private void Start()
    {
        open = true;

        brickScore = 0;
        a = 0;
        flyVec = new Vector3(0, 2f, 0);

        GetComponent<Animator>().SetBool("Fly", false);

        //Find the closest Brick
        closestBrick = null;
        brickContact = false;
    }
    private void Update()
    {
        brickText.text = brickScore.ToString();
    }
    private void LateUpdate()
    {
        Movement();
        Move_Update();
    }

    private void Move_Update()
    {
        if (isFly) //If bot is flying
        {
            if (isre_Fly && this.transform.position.y < 2)
            {
                this.transform.Translate(flyVec * Time.deltaTime * 1.5f);
            }
            //To evaluate current brick number & conditions
            if (brickScore > 0 && !botfalling)
            {
                if (!courotineBool)
                {
                    StartCoroutine(decreaseTheBricks(.15f));
                    courotineBool = true;
                }
                falling = false;
                this.transform.Translate(new Vector3(0, 0, 0));
            }
            if (brickScore == 0 || botfalling)
            {
                courotineBool = false;
                isre_Fly = false;
                falling = true;
            }
            //Stabilize the flying situations (Could be revized in the following days)
            if (this.transform.position.y > 2.1)
            {
                this.transform.Translate(new Vector3(0, 0, 0));
                isre_Fly = false;
                falling = true;
            }
            if (!isre_Fly && falling)
            {
                this.transform.Translate(-flyVec * Time.deltaTime);
            }
            if (this.transform.position.y == 0)
            {
                isFly = false;
            }
        }
        if (!open) //If Menu is open
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 0;
        }
    }
    //Decrease the brick numbers while bots are flying
    IEnumerator decreaseTheBricks(float i)
    {
        while (brickScore > 0)
        {
            yield return new WaitForSeconds(i);
            brickScore--;
            if (falling)
            {
                break;
            }
        }
    }
    // To find closest brick Transform
    Transform closestcricko()
    {
        multiplebricks = GameObject.FindGameObjectsWithTag("Bricks");
        float closestDistance = Mathf.Infinity;
        Transform trans = null;

        foreach (GameObject go in multiplebricks)
        {
            float currentDistance;
            currentDistance = Vector3.Distance(transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go.transform;
            }
        }
        return trans;
    }
    //Running Function
    private void Movement()
    {       
        closestBrick = closestcricko();
        if ((closestBrick.transform.position.z > this.transform.position.z) && start && closestBrick != null)
        {
            Vector3 LookAtGoal = new Vector3(closestBrick.position.x, this.transform.position.y, closestBrick.position.z);
            Vector3 direction = LookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 1f);
            GetComponent<Rigidbody>().velocity = transform.forward * 9.5f;

        }
        if((closestBrick == null && start) || (closestBrick.transform.position.z < this.transform.position.z && start))
        {
            Vector3 LookAtGoal = new Vector3(0, 0, 328);
            Vector3 direction = LookAtGoal - this.transform.position;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 1f);
            GetComponent<Rigidbody>().velocity = transform.forward * 9.5f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collect the brick objects to back of bots
        if (other.gameObject.tag == "Bricks")
        {
            brickScore += 3;
            a += 1;
            Instantiate(smallBrickPrefab).transform.SetParent(this.gameObject.transform.GetChild(2).transform);
            Destroy(other.gameObject);
            if (a == 1)
            {
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.rotation = this.gameObject.transform.GetChild(2).transform.rotation;
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.position = this.gameObject.transform.GetChild(2).transform.position;

            }

            if (a >= 2)
            {
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.rotation = this.gameObject.transform.GetChild(2).transform.rotation;
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.position = this.gameObject.transform.GetChild(2).transform.position;
            }
        }
        // To fly down the bots
        if (other.gameObject.tag == "BotPlane")
        {
            botfalling = true;
        }
        // Gameover Condition
        if (other.gameObject.tag == "PlaneRoad")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        // Running Condition
        if (other.gameObject.tag == "Plane")
        {
            GetComponent<Animator>().SetBool("Fly", false);
            isFly = false;
            isre_Fly = true;
            botfalling = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Flying Condition
        if (other.gameObject.tag == "Plane")
        {
            GetComponent<Animator>().SetBool("Fly", true);
            isFly = true;
        }
    }
    // Start Condition
    public void StartGameKey()
    {
        GetComponent<Animator>().SetTrigger("StartGame");
        start = true;
    }
    // Dance Condition - Bots are dancing when he/she landing to bonus cubes
    public void Dance()
    {
        GetComponent<Animator>().SetTrigger("BonusTrig");
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y + 180, this.transform.rotation.z);
        isFly = false;
        start = false;
    }
}
