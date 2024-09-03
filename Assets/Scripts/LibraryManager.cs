using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryManager : MonoBehaviour
{
    public static LibraryManager Instance { get; private set; }
    [SerializeField] GameObject credits;
    [SerializeField] List<ImageSlot> slotsList;
    private Dictionary<string, ImageSlot> slotsDict;
    private bool gameFinished;
    private bool finalDialogueStarted;

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

    private void Update()
    {
        if (gameFinished && !DialogueManager.Instance.DialogueActive && !finalDialogueStarted)
        {
            finalDialogueStarted = true;
            StartCoroutine(HandleGameFinished());
        }
    }

    private void InitializeDict()
    {
        slotsDict = new Dictionary<string, ImageSlot>();
        foreach (ImageSlot imgSlot in slotsList)
        {
            slotsDict.Add(imgSlot._name, imgSlot);
        }
    }

    public void UpdateSlot(string slotName, Sprite slotSprite)
    {
        if (slotsDict.TryGetValue(slotName, out ImageSlot slot))
        {
            slot._img.sprite = slotSprite;
            slot._text.text = slotName;
            slot._description.SetActive(true);
            slot.isSolved = true;
            //check if everything is solved
            CheckIfCompleted();
        }
        else
        {
            Debug.LogWarning($"Slot with name {slotName} not found");
        }
    }

    private void CheckIfCompleted()
    {
        bool AllAreTrue = true;

        foreach (ImageSlot slot in slotsList)
        {
            if (!slot.isSolved)
            {
                AllAreTrue = false;
                break;
            }
        }

        if (AllAreTrue)
        {
            Debug.Log("Game Finished!");
            gameFinished = true;
        }
    }

    private IEnumerator HandleGameFinished()
    {
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(2);
        Cursor.lockState = CursorLockMode.None;
        StoryManager.Instance.LaunchStory("FinalDialogue");
        yield return new WaitForSeconds(0.5f);
        while (DialogueManager.Instance.DialogueActive)
        {
            yield return null;
        }
        //Debug.Log("Launching Credits");
        credits.SetActive(true);
    }

}

[System.Serializable]
public class ImageSlot
{
    public string _name;
    public Image _img;
    public TextMeshProUGUI _text;
    public GameObject _description;
    public bool isSolved;
}
