using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedGoal : GoalScript
{
    [SerializeField] Animation animatedObject;
    [SerializeField] string animationToPlay = "";
    protected override void OnWin()
    {
        base.OnWin();
        animatedObject.Play(animationToPlay);
    }
}
