using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

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

    public List<LevelInfo> levelInfoList;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.WorldToViewportPoint(player.transform.position).x > 0.75 || mainCamera.WorldToViewportPoint(player.transform.position).y > 0.75 || mainCamera.WorldToViewportPoint(player.transform.position).x > -0.75 || mainCamera.WorldToViewportPoint(player.transform.position).y > -0.75)
        {
            if (player.transform.position.y < cutOffHeight)
            {

                KillPlayer();

            }

        }
    }

    public void KillPlayer()
    {
        player.GetComponent<PlayerController>().killPlayer();
    }

    public void winLevel()
    {

        Debug.Log("YOU WIN!!");

        goalMaterial.color = Color.Lerp(goalMaterial.color, endColor, Time.deltaTime);

        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime * slowSpeed);

        if (rb.velocity.magnitude < new Vector3(0.1f, 0.15f, 0.1f).magnitude)
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
        }

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

    }
}
