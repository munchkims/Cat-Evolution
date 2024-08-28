using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] List<NamedSprite> ItemSprites;
    private Image _image;
    private Dictionary<string, Sprite> spriteDict;

    private void Awake()
    {
        _image = GetComponent<Image>();
        spriteDict = new Dictionary<string, Sprite>();

        //populating the dictionary
        foreach (NamedSprite sp in ItemSprites)
        {
            spriteDict.Add(sp._name, sp._sprite);
        }
    }

    public void SetSprite(string spriteName)
    {
        if (spriteDict.TryGetValue(spriteName, out Sprite sprite))
        {
            _image.sprite = sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite with name {spriteName} not found");
        }

    }

    public Sprite GetSprite(string spriteName)
    {
        if (spriteDict.TryGetValue(spriteName, out Sprite sprite))
        {
            return sprite;
        }
        else
        {
            Debug.LogWarning($"Sprite with name {spriteName} not found");
            return null;
        }
    }

    public void ResetSprite()
    {
        _image.sprite = null;
    }
}
