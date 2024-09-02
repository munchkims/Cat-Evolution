using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioClip typingSound;
    [SerializeField] AudioClip uiSound;
    [SerializeField] AudioClip bookPageSound;
    [SerializeField] AudioClip trashSound;
    [SerializeField] AudioClip liquidDropSound;
    [SerializeField] AudioClip liquidComboSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip defaultCatSound;
    [SerializeField] AudioClip errorSound;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayUI()
    {
        audioSource.PlayOneShot(uiSound);
    }

    public void PlayTyping()
    {
        audioSource.PlayOneShot(typingSound, 0.1f);
    }

    public void PlayPageTurn()
    {
        audioSource.PlayOneShot(bookPageSound);
    }

    public void PlayTrashcan()
    {
        audioSource.PlayOneShot(trashSound);
    }

    public void PlayLiquidDrop()
    {
        audioSource.PlayOneShot(liquidDropSound);
    }

    public void PlayCombo()
    {
        audioSource.PlayOneShot(liquidComboSound);
    }

    public void PlaySuccess()
    {
        audioSource.PlayOneShot(successSound);
    }

    public void PlayDefaultCat()
    {
        audioSource.PlayOneShot(defaultCatSound);
    }

    public void PlayError()
    {
        audioSource.PlayOneShot(errorSound);
    }

}
