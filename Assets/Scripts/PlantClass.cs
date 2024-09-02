using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantClass : MonoBehaviour
{
    public Plants currentPlant;
    private SpriteManager spriteManager;

    private void Awake()
    {
        spriteManager = GetComponent<SpriteManager>();
        currentPlant = Plants.None;
    }

    public void ChangeType(string typeValue)
    {
        try
        {
            currentPlant = (Plants)Enum.Parse(typeof(Plants), typeValue);
        }

        catch (ArgumentException)
        {
            Debug.LogError($"Error: {typeValue} is not valid here");
        }

        spriteManager.SetSprite(typeValue);
        if (currentPlant == Plants.None)
        {
            LiquidController.Instance.FillPlantTube(true);
        }
        else
        {
            LiquidController.Instance.FillPlantTube(false);
        }
    }
}
