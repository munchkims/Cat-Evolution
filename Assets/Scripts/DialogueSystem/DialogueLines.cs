using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class DialogueLines : ScriptableObject
{
    public DialoguePart[] dialogueParts;

    [System.Serializable]
    public struct DialoguePart
    {
        [TextArea]
        public string line;
        public Emotion emotion;
    }
}
