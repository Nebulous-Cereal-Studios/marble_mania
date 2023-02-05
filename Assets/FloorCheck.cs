using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour
{
    public float distance;
    [SerializeField] bool grounded;
    public bool isOnFloor() {
        return getFloorDistance() < distance;
    }

    public float getFloorDistance() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 10, ~0b_0000_0010)) {
            return hit.distance;
        }
        return float.MaxValue;
    }

    private void Update() {
        if(Application.isEditor) {
            grounded = isOnFloor();
        }
    }
}
