using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollideEvents))]
public class WorldFeather : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CollideEvents>().OnTriggered.AddListener(() => {
            WingHandler wings = GameLogic.Instance.wingHandler;
            if(wings == null) {
                return;
            }
            gameObject.SetActive(false);
            var nextFeather = wings.GetNextFeather();
            if(nextFeather == null) {
                wings.RefillJumps();
                return;
            }
            wings.RefillJumps();
            wings.GetNextFeather().SetActive(true);
        });
        GameLogic.Instance.RegisterForReActivationOnDeath(gameObject);
    }
}
