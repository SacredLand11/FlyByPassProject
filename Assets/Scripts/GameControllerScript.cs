using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("Canvas")]
    [SerializeField] Image arrow;
    [Header("Variables")]
    [SerializeField] float playerPosz;
    [SerializeField] bool startBool = true;
    [SerializeField] public bool startmenuBool = true;
    void Awake()
    {
        Instantiate(RoadPrefab, transform.position, Quaternion.identity);
        Instantiate(player, transform.position, Quaternion.identity);
        Instantiate(bonusCubeParentPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(progressBarPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(startGamePrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(menuPrefab, transform.position, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        Instantiate(moneyTextPrefab, transform.position, Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-1, 2)*3, 0, 7.5f * i);
            Instantiate(BrickPrefab, randomSpawnPosition, Quaternion.identity).hideFlags = HideFlags.HideInHierarchy;
        }


    }
    private void Start()
    {
        Time.timeScale = 0;
        arrow = GameObject.Find("ProgressBarCanvas(Clone)").gameObject.transform.GetChild(2).GetComponent<Image>();
        StartCoroutine(Countdown(3));
    }
    private void Update()
    {
        if (!startBool)
        {
            Time.timeScale = 1;
        }
        if (!startmenuBool)
        {
            Time.timeScale = 0;
        }
        CurrentArrowPosition();
    }

    IEnumerator Countdown(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            GameObject.Find("StartGameText(Clone)/Seconds").GetComponent<Text>().text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        StartGame();
    }
    void StartGame()
    {
        PlayerMovement startgame = GameObject.Find("Character(Clone)").GetComponent<PlayerMovement>();
        startgame.StartGameKey();
        Destroy(GameObject.Find("StartGameText(Clone)"));
    }
    // To determine the progressbar level
    void CurrentArrowPosition()
    {
        playerPosz = GameObject.Find("Character(Clone)").transform.position.z;
        if (playerPosz <= 124)
        {
            arrow.rectTransform.position = new Vector3(175 + (700 / 111) * playerPosz, 1630, 0);
        }
    }
    public void setStartActive()
    {
        startBool = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }
}
