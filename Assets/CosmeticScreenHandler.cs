using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticScreenHandler : MonoBehaviour
{
    static CosmeticScreenHandler Instance;
    PlayerController player;

    public void Start() {
        Instance = this;
    }
}
