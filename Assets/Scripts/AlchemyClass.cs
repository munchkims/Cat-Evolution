using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyClass : MonoBehaviour
{
    public Alchemy currentElement;
    private SpriteManager spriteManager;

    private void Awake()
    {
        spriteManager = GetComponent<SpriteManager>();
        currentElement = Alchemy.None;
    }

    public void ChangeType(string typeValue)
    {
        try
        {
            currentElement = (Alchemy)Enum.Parse(typeof(Alchemy), typeValue);
        }

        catch (ArgumentException)
        {
            Debug.LogError($"Error: {typeValue} is not valid here");
        }

        spriteManager.SetSprite(typeValue);
    }
}
