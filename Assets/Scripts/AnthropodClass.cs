using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthropodClass : MonoBehaviour
{
    public Anthropods currentInsect;
    private SpriteManager spriteManager;

    private void Awake()
    {
        spriteManager = GetComponent<SpriteManager>();
        currentInsect = Anthropods.None;
    }

    public void ChangeType(string typeValue)
    {
        try
        {
            currentInsect = (Anthropods)Enum.Parse(typeof(Anthropods), typeValue);
        }

        catch (ArgumentException)
        {
            Debug.LogError($"Error: {typeValue} is not valid here");
        }

        spriteManager.SetSprite(typeValue);
        if (currentInsect == Anthropods.None)
        {
            LiquidController.Instance.FillInsectTube(true);
        }
        else
        {
            LiquidController.Instance.FillInsectTube(false);
        }
    }
}
