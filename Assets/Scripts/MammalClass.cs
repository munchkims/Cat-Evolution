using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammalClass : MonoBehaviour
{
    public Mammals currentMammal;
    private SpriteManager spriteManager;

    private void Awake()
    {
        spriteManager = GetComponent<SpriteManager>();
        currentMammal = Mammals.None;
    }

    public void ChangeType(string typeValue)
    {
        try
        {
            currentMammal = (Mammals)Enum.Parse(typeof(Mammals), typeValue);
        }

        catch (ArgumentException)
        {
            Debug.LogError($"Error: {typeValue} is not valid here");
        }

        spriteManager.SetSprite(typeValue);
    }
}
