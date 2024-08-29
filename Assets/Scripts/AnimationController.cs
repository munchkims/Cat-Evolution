using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Happy()
    {
        animator.SetTrigger("Jump");
    }

    public void Wave()
    {
        animator.SetTrigger("Wave");
    }

    public void Sad()
    {
        animator.SetTrigger("Sad");
    }
}
