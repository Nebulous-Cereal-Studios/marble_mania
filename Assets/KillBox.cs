using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBox : MonoBehaviour
{
    void OnCollisionEnter(Collision other) {
        if(other.gameObject == GameLogic.Instance.player) {
            GameLogic.Instance.KillPlayer();
        }
    }
    void OnTriggerEnter(Collider other) {
        if(other.gameObject == GameLogic.Instance.player) {
            GameLogic.Instance.KillPlayer();
        }
    }
}
