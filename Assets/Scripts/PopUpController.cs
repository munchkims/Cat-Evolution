using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    public static PopUpController Instance { get; private set; }
    [SerializeField] TextMeshProUGUI popUpText;
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

    public void ShowBubble(string _text)
    {
        popUpText.text = _text;
        StartCoroutine(BubbleRoutine());
        //animator.SetTrigger("PopUp");
    }

    private IEnumerator BubbleRoutine()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator.SetTrigger("PopUp");
        yield return new WaitForSeconds(3.25f);
        Cursor.lockState = CursorLockMode.None;
    }
}
