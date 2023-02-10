using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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

    public bool canMove = true;
    public bool canScoreGoal = false;

    public bool isMoving;
    public bool isCrouching;
    public bool isRunning;

    public Camera mainCamera;

    public Material normalMaterial;
    public Material flashingMaterial;

    public Vector3 speedcap;
    float force = 100;

    private float flashVal;

    [SerializeField] Vector3 verticalDirection;
    [SerializeField] Vector3 horizontalDirection;

    private void Start()
    {
        verticalDirection = PlayerDataContainer.Instance.data.verticalDirection.normalized;
        horizontalDirection = PlayerDataContainer.Instance.data.horizontalDirection.normalized;

        speed = walk;
        flashVal = 0;
        canMove = true;
    }
    private void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = (horizontal * horizontalDirection) + (vertical * verticalDirection);

        //cc.Move((moveVector) * speed * Time.deltaTime);
        rb.AddForce(moveVector * Time.deltaTime * force, ForceMode.Acceleration);
        // Determines if the speed = run or walk
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = run;
            isRunning = true;
        }

        //Digusting coded speedcap; I am not proud of this code
        if (rb.velocity.x > speedcap.x) { Debug.Log("REACING X SPEEDCAP"); rb.velocity = new Vector3(speedcap.x, rb.velocity.y, rb.velocity.z); }
        if (rb.velocity.y > speedcap.y) { Debug.Log("REACING Y SPEEDCAP"); rb.velocity = new Vector3(rb.velocity.x, speedcap.y, rb.velocity.z); }
        if (rb.velocity.z > speedcap.z) { Debug.Log("REACING Z SPEEDCAP");  rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speedcap.z); }

        // Detects if the player is moving.
        // Useful if you want footstep sounds and or other features in your game.

        //rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, Time.deltaTime / slowdownSpeed);

        if (!canMove)
        {
            flashVal = Mathf.Lerp(flashVal, 25, Time.deltaTime * flashSpeed);
            flashingMaterial.SetFloat("_FlashSpeed", flashVal);
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

    public void killPlayer(Transform newStart, Transform newEnd)
    {

        start = newStart;
        end = newEnd;

        Debug.Log("YOU DIED!!");

        rb.velocity = Vector3.zero;

        stopMovingPlayerTimer(3f);

        player.transform.position = start.position;

    }

    public void Jump(float force) {
        Vector3 vel = rb.velocity;
        vel.y = 0;
        rb.velocity = vel;
        rb.AddForce(new Vector3(0, force, 0), ForceMode.Impulse);
    }

}
