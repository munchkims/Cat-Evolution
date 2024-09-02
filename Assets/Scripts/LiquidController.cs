using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidController : MonoBehaviour
{
    public static LiquidController Instance { get; private set; }
    [SerializeField] Animator mammalAnimator;
    [SerializeField] Animator insectAnimator;
    [SerializeField] Animator plantAnimator;
    [SerializeField] Animator alchemyAnimator;

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

    public void FillMammalTube(bool makeEmpty)
    {
        if (makeEmpty)
        {
            mammalAnimator.SetBool("GetFilled", false);
        }
        else
        {
            mammalAnimator.SetBool("GetFilled", true);
        }
    }

    public void FillInsectTube(bool makeEmpty)
    {
        if (makeEmpty)
        {
            insectAnimator.SetBool("GetFilled", false);
        }
        else
        {
            insectAnimator.SetBool("GetFilled", true);
        }
    }

    public void FillPlantTube(bool makeEmpty)
    {
        if (makeEmpty)
        {
            plantAnimator.SetBool("GetFilled", false);
        }
        else
        {
            plantAnimator.SetBool("GetFilled", true);
        }
    }

    public void FillAlchemyTube(bool makeEmpty)
    {
        if (makeEmpty)
        {
            alchemyAnimator.SetBool("GetFilled", false);
        }
        else
        {
            alchemyAnimator.SetBool("GetFilled", true);
        }
    }

    public void FillAll()
    {
        mammalAnimator.SetBool("GetFilled", true);
        insectAnimator.SetBool("GetFilled", true);
        plantAnimator.SetBool("GetFilled", true);
        alchemyAnimator.SetBool("GetFilled", true);
    }

    public void UnfillAll()
    {
        mammalAnimator.SetBool("GetFilled", false);
        insectAnimator.SetBool("GetFilled", false);
        plantAnimator.SetBool("GetFilled", false);
        alchemyAnimator.SetBool("GetFilled", false);
    }
}
