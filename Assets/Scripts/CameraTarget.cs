using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    bool overrideFloor = true;
    [SerializeField] Transform player;
    float floor = 0f;

    private void Start() {
        floor = player.position.y;
        if(overrideFloor) {
            floor = GameLogic.Instance.cutOffHeight + 5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        var pos = player.position;

        pos.y = Mathf.Clamp(pos.y, floor, float.MaxValue);

        transform.position = pos;
    }
}
