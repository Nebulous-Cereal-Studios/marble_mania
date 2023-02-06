using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    List<Transform> points = new List<Transform>();
    int goalPoint = 0;
    [SerializeField] float tolerance = 0.5f;
    [SerializeField] float speed = 2;
    [SerializeField] float stopTime;
    float timer;

    private void Start() {
        points.Clear();
        foreach(Transform child in transform.parent) {
            if(child.gameObject == gameObject) {
                continue;
            }
            points.Add(child);
        }
    }

    private void Update() {
        if(timer == 0) {
            var direction = -(transform.localPosition - points[goalPoint].localPosition).normalized;
            transform.localPosition += direction * speed * Time.deltaTime;
            
            if((transform.localPosition - points[goalPoint].localPosition).magnitude < tolerance) {
                goalPoint++;
                timer = stopTime;
            }
            if(goalPoint >= points.Count) {
                goalPoint = 0;
            }
            return;
        }

        timer -= Time.deltaTime;
        if(timer <= 0) {
            timer = 0;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject == GameLogic.Instance.player) {
            other.transform.parent.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject == GameLogic.Instance.player) {
            other.transform.parent.SetParent(null);
        }
    }
}
