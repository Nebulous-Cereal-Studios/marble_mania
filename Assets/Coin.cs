using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollideEvents))]
public class Coin : MonoBehaviour
{
    [SerializeField] int value;
    [SerializeField] ParticleSystem particles;
    bool active = true;
    void Start()
    {
        active = true;
        Renderer coinRenderer = transform.GetChild(0).GetComponent<Renderer>();
        GetComponent<CollideEvents>().OnTriggered.AddListener(() => {
            if(!active)
                return;
            active = false;
            PlayerDataContainer.Instance.data.currency += value;
            coinRenderer.enabled = false;
            particles.Play();
        });
        GameLogic.Instance.OnDeath.AddListener(() => {
            active = true;
            coinRenderer.enabled = true;
        });
    }
}
