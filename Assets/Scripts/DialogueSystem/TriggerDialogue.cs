using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogue : MonoBehaviour
{
    [SerializeField] DialogueLines dialogueLines;
    // Start is called before the first frame update
    void Start()
    {
        StartConvo();
    }

    public void StartConvo()
    {
        DialogueManager.Instance.StartDialogue(dialogueLines);
    }
}
