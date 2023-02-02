using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{

    public GameLogic gameLogic;
    public GameObject player;

    private bool won;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!won)
        {

            if (collision.gameObject.name == player.name)
            {
                gameLogic.winLevel();

                won = true;
            }

        }
        
    }
}
