using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryManager : MonoBehaviour
{
    public static LibraryManager Instance { get; private set; }
    [SerializeField] List<ImageSlot> slotsList;
    private Dictionary<string, ImageSlot> slotsDict;

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
        }
        else
        {
            Debug.LogWarning($"Slot with name {slotName} not found");
        }
    }

}

[System.Serializable]
public struct ImageSlot
{
    public string _name;
    public Image _img;
    public TextMeshProUGUI _text;
}
