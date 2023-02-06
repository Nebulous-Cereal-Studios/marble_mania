using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PeriodicUpdateMatOffset : MonoBehaviour
{
    [SerializeField] Vector2 Offsets;
    [SerializeField] float time;
    float timer;
    Material material;

    private void Start() {
        material = GetComponent<Renderer>().material;
    }

    private void Update() {
        timer += Time.deltaTime;
        if(timer > time) {
            timer = 0;
            material.mainTextureOffset += Offsets;
        }
    }
}
