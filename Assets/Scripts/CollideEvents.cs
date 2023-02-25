using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollideEvents : MonoBehaviour
{
    public UnityEvent OnTriggered = new UnityEvent();
    public UnityEvent WhilePlayerTriggered = new UnityEvent();

    void OnTriggerEnter(Collider other) {
        OnTriggered.Invoke();
    }
    private void OnTriggerStay(Collider other) {
        OnTriggered.Invoke();
    }
}
