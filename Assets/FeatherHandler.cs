using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherHandler : MonoBehaviour
{
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
}
