using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject smallBrickPrefab; //Collected Bricks
    public GameObject openMenuPrefab; //Menu Prefab
    public GameObject RestartPrefab; //Restart Prefab
    [Header("Texts")]
    public Text brickText;

    //Brick variables
    int brickScore;
    int a;
    //Menu Variable
    public int openint;
    //Motion Variable
    float yaw = 0.0f;
    //Flying Variables
    bool isFly = false;
    bool isre_Fly = true;
    bool falling = false;
    bool courotineBool = false;
    Vector3 flyVec;
    //Start Variable
    bool start = false;
    bool motion = true;
    bool open;
    //Money Variable
    public bool moneycollect;

    //public List<Vector3> rightBrickPos = new List<Vector3>();
    //public List<Vector3> leftBrickPos = new List<Vector3>();
    private void Start()
    {
        moneycollect = false;

        openint = 0;
        open = true;

        brickScore = 0;
        a = 0; // List Number

        GetComponent<Animator>().SetBool("Fly", false);
        flyVec = new Vector3(0, 2f, 0);
    }
    private void Update()
    {
        /*
        if (a >= 1)
        {
            for (int i = 0  ; i < a; i++)
            {
                rightBrickPos[i] = new Vector3(this.transform.localPosition.x + .15f * i, this.transform.localPosition.y + 1.75f , this.transform.localPosition.z);
                leftBrickPos[i] = new Vector3(this.transform.position.x - .15f * i, this.transform.position.y + 1.75f, this.transform.position.z);
            }
        }
        */
        brickText.text = brickScore.ToString();
    }
    private void LateUpdate()
    {
        Movement();
        Move_Update();
    }
    private void Move_Update()
    {
        if (isFly)//If bot is flying
        {
            /*
            for (int i = 1; i < a; i++)
            {
                //Position
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).transform.position = rightBrickPos[i - 1];//new Vector3(this.transform.position.x + (i - 1) * Mathf.Cos(-this.transform.rotation.y), rightBrickPos[i - 1].y, this.transform.position.z + (i - 1) * Mathf.Sin(-this.transform.rotation.y)); 
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.transform.GetChild(1).transform.Rotate(0, 0, this.transform.rotation.y);//new Vector3(this.transform.position.x + (i - 1) * Mathf.Cos(-this.transform.rotation.y), rightBrickPos[i - 1].y, this.transform.position.z + (i - 1) * Mathf.Sin(-this.transform.rotation.y)); 
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).transform.Rotate(0, 0, this.transform.rotation.y);//new Vector3(this.transform.position.x + (i - 1) * Mathf.Cos(-this.transform.rotation.y), rightBrickPos[i - 1].y, this.transform.position.z + (i - 1) * Mathf.Sin(-this.transform.rotation.y)); 
                this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(i).gameObject.transform.GetChild(0).transform.position = leftBrickPos[i - 1];// new Vector3(this.transform.position.x - (i - 1) * Mathf.Cos(-this.transform.rotation.y), rightBrickPos[i - 1].y, this.transform.position.z - (i - 1) * Mathf.Sin(-this.transform.rotation.y));  
            }*/
            
            if (isre_Fly && this.transform.position.y < 2f)
            {
                this.transform.Translate(flyVec * Time.deltaTime * 1.5f);
            }
            //To evaluate current brick number & conditions
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
            //Stabilize the flying situations (Could be revized in the following days)
            if (this.transform.position.y > 1.9f)
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
        if (start)//If Menu is open
        {
            GetComponent<Rigidbody>().velocity = transform.forward * 10;
            if (!open)
            {
                GetComponent<Rigidbody>().velocity = transform.forward * 0;
            }
        }
    }
    //Decrease the brick numbers while bots are flying
    IEnumerator decreaseTheBricks(float i)
    {
        int brickvision = 0;
        while (brickScore > 0)
        {
            yield return new WaitForSeconds(i);
            brickScore--;
            brickvision++;
            if(brickvision == 3)
            {
                Destroy(this.gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).gameObject);
                //rightBrickPos.Remove(rightBrickPos[a - 1]);
                //leftBrickPos.Remove(leftBrickPos[a - 1]);
                a--;
                brickvision = 0;
            }
            if(falling)
            {
                break;
            }
        }
    }
    //Running Function
    private void Movement()
    {
        if (Input.touchCount > 0 && motion)
        {
            yaw += Input.GetTouch(0).deltaPosition.x * 10 * Time.deltaTime;
            yaw = Mathf.Clamp(yaw, -80, 80);
            this.transform.eulerAngles = new Vector3(0, yaw, 0);
        }
    }
    //Start Condition
    public void StartGameKey()
    {
        GetComponent<Animator>().SetTrigger("StartGame");
        start = true;
    }
    //Dance Condition - Player is dancing when he/she landing to bonus cubes
    public void Dance()
    {
        GetComponent<Animator>().SetTrigger("BonusTrig");
        this.transform.eulerAngles = new Vector3(this.transform.rotation.x, this.transform.rotation.y + 180, this.transform.rotation.z);
        isFly = false;
        start = false;
        motion = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Collect the brick objects to back of the player
        if (other.gameObject.tag == "Bricks")
        {
            //rightBrickPos.Add(new Vector3(this.transform.position.x + .15f * a, 1.75f, this.transform.position.z));
            //leftBrickPos.Add(new Vector3(this.transform.position.x - .15f * a, 1.75f, this.transform.position.z));
            brickScore += 3;
            a += 1;
            Instantiate(smallBrickPrefab).transform.SetParent(GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).transform);
            Destroy(other.gameObject);

            if (a == 1)
            {
                GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.rotation = GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).transform.rotation;
                GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.position = GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).transform.position;

            }

            if (a >= 2)
            {
                GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.rotation = GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).transform.rotation;
                GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).gameObject.transform.GetChild(a).transform.position = GameObject.Find("Character(Clone)").gameObject.transform.GetChild(2).transform.position;
            }
        }
        // Gameover Condition
        if (other.gameObject.tag == "PathRoad")
        {
            Instantiate(RestartPrefab);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>().enabled = false;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Running Condition
        if (other.gameObject.tag == "Plane")
        {
            GetComponent<Animator>().SetBool("Fly", false);
            isFly = false;
            isre_Fly = true;
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
    //Money & Scene Transition
    public void NextLevelScene()
    {
        GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().moneycollect = true;
        GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().StartCoroutine(SceneTransition());
        int moneyvariable = int.Parse(GameObject.FindGameObjectWithTag("LevelRoad").GetComponent<FinishScript>().moneyText.text) + GameObject.FindGameObjectWithTag("LevelRoad").GetComponent<FinishScript>().money;
        GameObject.FindGameObjectWithTag("LevelRoad").GetComponent<FinishScript>().moneyText.text = moneyvariable.ToString();
        GameObject.Find("ClaimButton(Clone)").gameObject.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }
    //Scene Transition Function
    public IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(4);
        if(SceneManager.GetActiveScene().name == "Lv.3")
        {
            SceneManager.LoadScene(0);
        }
        if(SceneManager.GetActiveScene().name != "Lv.3")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
    }
    //Restart Condition
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //Menu Condition
    public void OpenMenu()
    {
        if(GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().openint < 1)
        {
            Instantiate(openMenuPrefab);
        }
        GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().open = false;
        GameObject.Find("Bot(Clone)").GetComponent<BotMovementScript>().open = false;
        GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().openint++;
    }
    public void CloseMenu()
    {
        Destroy(GameObject.Find("OpenMenuCanvas(Clone)"));
        GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().open = true;
        GameObject.Find("Bot(Clone)").GetComponent<BotMovementScript>().open = true;
        GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>().openint = 0;
    }
}
