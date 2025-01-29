using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public Animator animator;
    public void Play(string animationName)
    {
        animator.Play(animationName);
    }
}
