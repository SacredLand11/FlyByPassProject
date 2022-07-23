using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject RoadPrefab;
    public GameObject BrickPrefab;
    public GameObject player;
    public GameObject progressBarPrefab;
    public GameObject startGamePrefab;
    public GameObject bonusCubeParentPrefab;
    public GameObject menuPrefab;
    public GameObject moneyTextPrefab;
    public GameObject bot;
    public GameObject bot2;
    [Header("Canvas")]
    [SerializeField] Image arrow;
    [SerializeField] Text levelText;
    [Header("Variables")]
    [SerializeField] float playerPosz;
    [SerializeField] bool startBool = true;
    [SerializeField] public bool startmenuBool = true;


    static bool hasSpawned = false;
    void Awake()
    {
        //Instantiate the Objects
            //To see clear hierarchy hide some objects
        Instantiate(RoadPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(bonusCubeParentPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(progressBarPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(startGamePrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(menuPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
            //Objects which are needed in the hierarchy
        Instantiate(player);
        Instantiate(bot);
        Instantiate(bot2);
        // To keep money to next scenes
        if (hasSpawned)
        {
            return;
        }
        Instantiate(moneyTextPrefab, transform.position, Quaternion.identity);
        DontDestroyOnLoad(GameObject.Find("MoneyTextCanvas(Clone)"));
        hasSpawned = true;

    }
    private void Start()
    {
        //Start Condition
        Time.timeScale = 0;
        arrow = GameObject.Find("ProgressBarCanvas(Clone)").gameObject.transform.GetChild(2).GetComponent<Image>();
        levelText = GameObject.Find("ProgressBarCanvas(Clone)").gameObject.transform.GetChild(3).GetComponent<Text>();
        levelText.text = SceneManager.GetActiveScene().name;
        // Ready Condition
        StartCoroutine(Countdown(3));
    }
    private void Update()
    {
        //Time Conditions
        if (!startBool)
        {
            Time.timeScale = 1;
        }
        if (!startmenuBool)
        {
            Time.timeScale = 0;
        }
        //To determine the rest of the path
        CurrentArrowPosition();
    }
    //Countdown Function when the player press the ready
    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            GameObject.Find("StartGameText(Clone)/Seconds").GetComponent<Text>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        //To active bots and player
        StartGame();
    }
    //To active the movement
    void StartGame()
    {
        PlayerMovement startgame = GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>();
        BotMovementScript startbotgame = GameObject.Find("Bot(Clone)").GetComponent<BotMovementScript>();
        BotMovementScript startbot2game = GameObject.Find("Bot2(Clone)").GetComponent<BotMovementScript>();
        startgame.StartGameKey();
        startbotgame.StartGameKey();
        startbot2game.StartGameKey();
        Destroy(GameObject.Find("StartGameText(Clone)"));
    }
    //To deactive the movement
    public void setStartActive()
    {
        startBool = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
    // To determine the progressbar level
    void CurrentArrowPosition()
    {
        if (GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>().enabled)
        {
            playerPosz = GameObject.Find("Character(Clone)").transform.position.z;
            if (playerPosz <= 261)
            {
                arrow.rectTransform.position = new Vector3(175 + 2.75f * playerPosz, 1630, 0);
            }
        }
        if(!GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowCamera>().enabled)
        {
            playerPosz = 0;
        }
    }

}
