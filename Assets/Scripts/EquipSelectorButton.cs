using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSelectorButton : MonoBehaviour
{
    private void Start() {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    void OnClick() {
        
    }
}