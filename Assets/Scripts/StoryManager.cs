using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }
    [SerializeField] List<DialogueStory> storyParts;
    private Dictionary<string, DialogueLines> storyDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeDict();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDict()
    {
        storyDict = new Dictionary<string, DialogueLines>();
        foreach (DialogueStory story in storyParts)
        {
            storyDict.Add(story._name, story.lines);
        }
    }

    public void LaunchStory(string storyName)
    {
        if (storyDict.TryGetValue(storyName, out DialogueLines lines))
        {
            StartConvo(lines);
        }
        else
        {
            Debug.LogWarning($"Story with name {storyName} not found");
        }
    }

    private void StartConvo(DialogueLines receivedLines)
    {
        DialogueManager.Instance.StartDialogue(receivedLines);
    }
}

[System.Serializable]
public struct DialogueStory
{
    public DialogueLines lines;
    public string _name;
}
