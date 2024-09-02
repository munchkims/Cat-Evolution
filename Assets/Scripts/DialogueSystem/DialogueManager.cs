using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject dialogueBox;
    // reference to a narrator GameObj or a class with animator
    [SerializeField] GameObject avatar;
    private AnimationController avatarController;

    //add typewritereffect
    private string d;
    private bool skipLineTriggered;
    [SerializeField] private float typeSpeed;
    [SerializeField] private const float MAX_TYPE_TIME = 0.1f;
    //reference to AudioManager for typingsound
    public bool DialogueActive { get; private set; }

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

    public void StartDialogue(DialogueLines dialogueLines)
    {
        DialogueActive = true;
        dialogueBox.SetActive(true);
        avatar.SetActive(true);
        avatarController = avatar.GetComponent<AnimationController>();
        StartCoroutine(RunDialogue(dialogueLines, 0));
    }

    IEnumerator RunDialogue(DialogueLines dialogueLines, int section)
    {
        for (int i = 0; i < dialogueLines.dialogueParts.Length; i++)
        {
            //send emotions to control here
            Debug.Log(dialogueLines.dialogueParts[section].emotion);
            switch (dialogueLines.dialogueParts[section].emotion)
            {
                case Emotion.Wave:
                    avatarController.Wave();
                    break;
                case Emotion.Happy:
                    avatarController.Happy();
                    break;
                case Emotion.Sad:
                    avatarController.Sad();
                    break;
                case Emotion.Idle:
                    break;
            }

            int maxVisibleChars = 0;
            d = dialogueLines.dialogueParts[section].line;
            dialogueText.text = d;
            dialogueText.maxVisibleCharacters = maxVisibleChars;

            foreach (char c in d.ToCharArray())
            {
                if (skipLineTriggered)
                {
                    skipLineTriggered = false;
                    break;
                }
                maxVisibleChars++;
                dialogueText.maxVisibleCharacters = maxVisibleChars;
                //typing sound play or emotion sound
                AudioManager.Instance.PlayTyping();
                yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
            }
            dialogueText.maxVisibleCharacters = d.Length;
            section++;

            while (skipLineTriggered == false)
            {
                yield return null;
            }
            skipLineTriggered = false;
            if (i == dialogueLines.dialogueParts.Length - 1)
            {
                EndConvo();
            }
        }
    }

    private void EndConvo()
    {
        dialogueBox.SetActive(false);
        //deactivate avatar
        avatar.SetActive(false);
        skipLineTriggered = false;
        DialogueActive = false;
    }

    public void SkipLine()
    {
        skipLineTriggered = true;
    }
}


public enum Emotion
{
    Sad,
    Wave,
    Happy,
    Idle
}
