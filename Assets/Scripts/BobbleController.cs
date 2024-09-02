using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbleController : MonoBehaviour
{
    public static BobbleController Instance { get; private set; }

    private Animator animator;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Shrink()
    {
        animator.SetBool("IsShrunk", true);
    }

    public void UnShrink()
    {
        animator.SetBool("IsShrunk", false);
    }
}
