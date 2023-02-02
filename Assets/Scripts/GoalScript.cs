using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    private bool won;

    private void OnCollisionEnter(Collision collision)
    {
        if (!won)
        {

            if (collision.gameObject == GameLogic.Instance.player)
            {
                OnWin();

            }

        }
        
    }

    protected virtual void OnWin() {
        GameLogic.Instance.winLevel();
        won = true;
    }
}
