using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DisapearImage : MonoBehaviour
{
    Image img;
    float direction = 0;
    [SerializeField] float speed;

    void Start() {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(direction == 0) {
            return;
        }
        Color col = img.color;

        col.a += Time.deltaTime * speed * direction;
        
        if(col.a <= 0) {
            col.a = 0;
        }
        if(col.a >= 1) {
            col.a = 1;
        }
        img.color = col;
    }

    public void StartDissapearing() {
        direction = -1;
    }

    public virtual void StartReapearing() {
        direction = 1;
    }

    protected virtual void OnDisapear() {
        
    }
}
