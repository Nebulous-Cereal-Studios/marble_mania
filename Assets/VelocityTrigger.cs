using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTrigger : MonoBehaviour
{
    [SerializeField] float incomingVelocity;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 correctiveVelocity;
    [SerializeField] Vector3 playerDirection;
    [SerializeField] float playerspeed;

    private void OnTriggerStay(Collider other) {
        if(other.gameObject != GameLogic.Instance.player) {
            Debug.Log("no");
            return;
        }
        Debug.Log("yes");
        var player = GameLogic.Instance.player.GetComponent<PlayerController>();
        playerspeed = player.rb.velocity.magnitude;
        playerDirection = player.rb.velocity.normalized;
        
        if((player.rb.velocity.normalized - direction).magnitude > 0.2) {
            Debug.Log("Direction did not match");
            return;
        }

        if(player.rb.velocity.magnitude > incomingVelocity) {
            player.rb.AddForce(correctiveVelocity * Time.deltaTime);
        }
    }
}
