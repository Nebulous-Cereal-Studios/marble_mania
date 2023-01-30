using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;
    public Rigidbody rb;

    public Transform start;
    public Transform end;

    [SerializeField] private GameObject player;
    [SerializeField] private float Sensitivity;

    private float speed;
    [SerializeField] private float walk;
    [SerializeField] private float run;

    public float slowdownSpeed;
    public float flashSpeed;

    public float cutOffHeight;
    public float winDistance;

    public bool canMove = true;

    public bool isMoving;
    public bool isCrouching;
    public bool isRunning;

    public Camera mainCamera;

    public Material normalMaterial;
    public Material flashingMaterial;

    private float flashVal;

    private void Start()
    {
        speed = walk;
        flashVal = 0;
        canMove = true;
    }
    private void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(horizontal, 0, vertical);

        //cc.Move((moveVector) * speed * Time.deltaTime);
        rb.AddForce(moveVector, ForceMode.Acceleration);
        // Determines if the speed = run or walk
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = run;
            isRunning = true;
        }

        // Detects if the player is moving.
        // Useful if you want footstep sounds and or other features in your game.
        
        //rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime / slowdownSpeed);

        if (mainCamera.WorldToViewportPoint(player.transform.position).x > 0.75 || mainCamera.WorldToViewportPoint(player.transform.position).y > 0.75 || mainCamera.WorldToViewportPoint(player.transform.position).x > -0.75 || mainCamera.WorldToViewportPoint(player.transform.position).y > -0.75)
        {
            if(player.transform.position.y < cutOffHeight)
            {

                killPlayer();

            }
            
        }
        

        if(Vector3.Distance(player.transform.position, end.position) <= winDistance)
        {

            Debug.Log("YOU WIN!!");

            //rb.isKinematic = true;

        }

        if (!canMove)
        {
            flashVal = Mathf.Lerp(flashVal, 25, Time.deltaTime * flashSpeed);

            Debug.Log(flashVal + "wwww");

            flashingMaterial.SetFloat("_FlashSpeed", flashVal);

            Debug.Log(flashingMaterial.GetFloat("_FlashSpeed") + "wdkwoaidkmowa");
        }
    }

    private void stopMovingPlayerTimer(float delay)
    {

        canMove = false;
        rb.isKinematic = true;
        setFlashingMaterial();

        Invoke("movePlayerAgain", delay);

    }

    private void movePlayerAgain()
    {

        canMove=true;

        rb.isKinematic = false;

        removeFlashingMaterial();

    }
    
    private void setFlashingMaterial()
    {
        player.GetComponent<Renderer>().material = flashingMaterial;

        flashVal = 0;

    }

    private void removeFlashingMaterial()
    {
        player.GetComponent<Renderer>().material = normalMaterial;
    }

    public void killPlayer()
    {

        Debug.Log("YOU DIED!!");

        rb.velocity = Vector3.zero;

        stopMovingPlayerTimer(3f);

        player.transform.position = start.position;

    }

}
