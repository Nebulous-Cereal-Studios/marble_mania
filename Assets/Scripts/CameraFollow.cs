using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject offsetObj;
    public GameObject playerObj;

    public Camera camera;

    public float distance;

    public int staticHeight;

    private float xOffest;
    private float yOffest;
    private float zHeight;

    // Start is called before the first frame update
    void Start()
    {
        xOffest = offsetObj.transform.position.x - playerObj.transform.position.x;
        yOffest = offsetObj.transform.position.z - playerObj.transform.position.z;
        zHeight = offsetObj.transform.position.y - playerObj.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (camera.WorldToViewportPoint(playerObj.transform.position).x > 0.75 || camera.WorldToViewportPoint(playerObj.transform.position).y > 0.75 || camera.WorldToViewportPoint(playerObj.transform.position).x > -0.75 || camera.WorldToViewportPoint(playerObj.transform.position).y > -0.75)
        {

            

        }

        offsetObj.transform.position = new Vector3(playerObj.transform.position.x + xOffest, zHeight, playerObj.transform.position.z + yOffest);

        //Debug.Log("Distance: " + Vector3.Distance(gameObject.transform.position, offsetObj.transform.position));

        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, offsetObj.transform.position, Time.deltaTime * 2);

    }
}
