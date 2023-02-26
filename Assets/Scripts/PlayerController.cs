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
    public FloorCheck floorCheck;

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
    float startForce = 300;
    float breakForce = 250;

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
        floorCheck = GetComponent<FloorCheck>();
    }
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveVector = (horizontal * horizontalDirection) + (vertical * verticalDirection);
        
        isMoving = moveVector.magnitude > 0f;

        if (isMoving)
        {
            rb.AddForce(moveVector * Time.deltaTime * force, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveVector.normalized * startForce * Time.deltaTime, ForceMode.Acceleration);
            Vector3 vel = rb.velocity;
            vel.y = 0;
            if(Mathf.Abs(rb.velocity.magnitude) > 0.5 && floorCheck.isOnFloor()) {
                Vector3 brakingForceVector = -rb.velocity.normalized * breakForce;
                rb.AddForce(brakingForceVector * Time.deltaTime, ForceMode.Acceleration);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = run;
            isRunning = true;
        }

        // Apply speedcap
        Vector3 clampedVelocity = rb.velocity;
        if (clampedVelocity.magnitude > speedcap.magnitude) {
            clampedVelocity = clampedVelocity.normalized * speedcap.magnitude;
        }
        rb.velocity = clampedVelocity;

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
