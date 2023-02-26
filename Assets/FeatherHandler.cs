using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherHandler : MonoBehaviour
{
    [SerializeField] Vector3 localOffset = Vector3.zero;
    [SerializeField] Camera cam;
    [SerializeField] bool startDisabled = true;
    // Start is called before the first frame update
    void Start()
    {
        if(startDisabled) {
            GameLogic.Instance.OnDeath.AddListener(() => {
                foreach(Transform child in transform) {
                    child.gameObject.SetActive(false);
                }
            });
        }
    }


    void Update() {
        // transform.localPosition = cam.WorldToScreenPoint(GameLogic.Instance.player.transform.position);
        // transform.localPosition += localOffset;
    }
}
