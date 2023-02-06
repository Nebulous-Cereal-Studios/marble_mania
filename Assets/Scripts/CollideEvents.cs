using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollideEvents : MonoBehaviour
{
    public UnityEvent OnTriggered = new UnityEvent();

    void OnTriggerEnter(Collider other) {
        OnTriggered.Invoke();
    }
}
