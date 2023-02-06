using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;

    public GameObject player;
    public Rigidbody rb;

    public Camera mainCamera;
    public CinemachineVirtualCamera CNCamera;

    public float cutOffHeight;

    public Material goalMaterial;
    public Color startColor;
    public Color endColor;

    public Transform start;
    public Transform end;
    public int slowSpeed;

    public int winDistance;

    private bool ifWinning;
    private bool won;
    public int deaths = 0;

    public List<LevelInfo> levelInfoList;
    public UnityEvent OnDeath = new UnityEvent();
    public int nextLevelIndex;

    // Awake is called before start
    void Awake()
    {
        // if(Instance != null) {
        //     Destroy(this);
        //     return;
        // }
        Instance = this;
        rb = player.gameObject.GetComponent<Rigidbody>();

        goalMaterial.color = startColor;
        //DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R) && !won)
        {
            KillPlayer();
        }

        if (Input.GetKeyDown(KeyCode.L)) {
            loadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (mainCamera.WorldToViewportPoint(player.transform.position).x > 0.75 || mainCamera.WorldToViewportPoint(player.transform.position).y > 0.75 || mainCamera.WorldToViewportPoint(player.transform.position).x > -0.75 || mainCamera.WorldToViewportPoint(player.transform.position).y > -0.75)
        {
            if (player.transform.position.y < cutOffHeight)
            {

                KillPlayer();

            }

        }

        if (ifWinning)
        {

            Vector3 newGoalPos = new Vector3(end.position.x, end.position.y + 1, end.position.z);

            player.transform.position = Vector3.Lerp(player.transform.position, newGoalPos, Time.deltaTime * slowSpeed);
            goalMaterial.color = Color.Lerp(goalMaterial.color, endColor, Time.deltaTime);

            if (player.transform.position == end.position)
            {

                ifWinning = false;
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;

            }

        }

    }

    public void KillPlayer()
    {
        OnDeath.Invoke();
        player.GetComponent<PlayerController>().killPlayer(start, end);
        deaths++;
        PlayerDataContainer.Instance.data.deaths++;
    }

    public void winLevel()
    {

        Debug.Log("YOU WIN!!");

        

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;

        startWinLevelDelay(3f);

    }

    public void startWinLevelDelay(float delay)
    {
        ifWinning = true;
        won = true;
        Invoke("stopWinLevelDelay", delay);
    }

    public void stopWinLevelDelay()
    {
        ifWinning = false;
        loadLevel(nextLevelIndex);
    }

    public void readSceneLogic(int index)
    {
        if(levelInfoList[index] != null)
        {

            LevelInfo levelInfo = levelInfoList[index];

            CNCamera.m_Lens.OrthographicSize = levelInfo.zoom;

        }
        else
        {
            loadLevel(0);
            Debug.Log("Level " + index + " does not exist. Sending player back to level 1.");
        }
    }

    public void loadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void loadMenu() {
        loadLevel(0);
    }
}
