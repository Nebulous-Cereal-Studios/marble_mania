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
    [SerializeField] Transform featherDisplay;
    [SerializeField] float floorTime = 0f;
    [SerializeField] float reapearTime = 1f;

    void Start() {
        player = GameLogic.Instance.player.GetComponent<FloorCheck>();
        featherDisplay.gameObject.SetActive(true);
        jumpsLeft = featherDisplay.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        bool playerOnFloor;
        if(playerOnFloor = player.isOnFloor() && jumpsLeft < featherDisplay.childCount) {
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
        if(Input.GetKeyDown(KeyCode.Space)) {
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
    }
}
