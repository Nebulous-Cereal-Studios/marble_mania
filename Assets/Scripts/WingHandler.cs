using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingHandler : MonoBehaviour
{
    FloorCheck player;
    [SerializeField] Animator Left;
    [SerializeField] Animator Right;
    [SerializeField] int jumpsLeft = 3;
    [SerializeField] Vector3 offset = Vector3.zero;
    [SerializeField] float force = 5;
    public Transform featherDisplay;
    [SerializeField] float floorTime = 0f;
    [SerializeField] float reapearTime = 1f;
    [SerializeField] bool active;
    [SerializeField] float slowDecentForce = 8f;

    bool FlightKeyPressed {
        get {
            return Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0);
        }
    }

    bool FlightKeyHeld {
        get {
            return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0);
        }
    }

    float heldSpaceDuration = 0f;
    int jumps {
        get {
            int count = 0;
            foreach(Transform child in featherDisplay) {
                if(child.gameObject.activeSelf) {
                    count++;
                }
            }
            return count;
        }
    }

    public GameObject GetNextFeather() {
        for(int i = 0; i < featherDisplay.childCount; i++) {
            var go = featherDisplay.GetChild(i).gameObject;
            if(!go.activeSelf) {
                return go;
            }
        }
        return null;
    }

    void Start() {
        GameLogic.Instance.wingHandler = this;
        player = GameLogic.Instance.player.GetComponent<FloorCheck>();
        featherDisplay.gameObject.SetActive(true);
        jumpsLeft = jumps;
    }

    void SetWingRenderStatus(bool visible) {
        foreach(var wing in GetComponentsInChildren<Renderer>()) {
            wing.enabled = visible;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!active) {
            SetWingRenderStatus(active);
            return;
        }
        if(jumps == 0) {
            SetWingRenderStatus(false);
            return;
        }else {
            SetWingRenderStatus(true);
        }


        bool playerOnFloor;
        if(playerOnFloor = player.isOnFloor() && jumpsLeft < jumps) {
            floorTime += Time.deltaTime;
            if(floorTime > reapearTime) {
                floorTime = 0;
                featherDisplay.GetChild(jumpsLeft).GetComponent<DisapearImage>().StartReapearing();
                jumpsLeft++;
            }
        }else {
            floorTime = 0;
        }
        transform.position = player.transform.position + offset;
        if(FlightKeyPressed) {
            if(jumpsLeft > 0) {
                jumpsLeft--;
                Left.SetTrigger("Jump");
                Right.SetTrigger("Jump");
                player.GetComponent<PlayerController>().Jump(force);
                featherDisplay.GetChild(jumpsLeft).GetComponent<DisapearImage>().StartDissapearing();
                if(playerOnFloor) {
                    player.transform.position += new Vector3(0, player.getFloorDistance());
                }
            }
        }
        if(FlightKeyHeld && !playerOnFloor) {
            heldSpaceDuration += Time.deltaTime;
            if(heldSpaceDuration > 0.1f) {
                Debug.Log("up");
                player.GetComponent<Rigidbody>().AddForce(new Vector3(0, slowDecentForce * Time.deltaTime, 0));
                Left.SetBool("SlowedDecent", true);
                Right.SetBool("SlowedDecent", true);
            }
            return;
        }
        Left.SetBool("SlowedDecent", false);
        Right.SetBool("SlowedDecent", false);
        heldSpaceDuration = 0;
    }

    public void RefillJumps() {
        jumpsLeft = jumps;
        foreach(Transform child in featherDisplay) {
            if(!child.gameObject.activeSelf) {
                continue;
            }
            child.GetComponent<DisapearImage>().StartReapearing();
        }

    }
}
