using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    [SerializeField] List<Combo> validCombinations;
    [SerializeField] MammalClass mammalClass;
    [SerializeField] AnthropodClass anthropodClass;
    [SerializeField] PlantClass plantClass;
    [SerializeField] AlchemyClass alchemyClass;

    private SpriteManager spriteManager;
    private bool isMatchFound;

    private void Awake()
    {
        spriteManager = GetComponent<SpriteManager>();
    }

    public void ComboCheck()
    {
        isMatchFound = false;
        Mammals thisMammal = mammalClass.currentMammal;
        Anthropods thisAnthropod = anthropodClass.currentInsect;
        Plants thisPlant = plantClass.currentPlant;
        Alchemy thisElement = alchemyClass.currentElement;

        Combination thisCombination = new Combination(thisMammal, thisAnthropod, thisPlant, thisElement);

        foreach (Combo validCombo in validCombinations)
        {
            if (validCombo.combination.Matches(thisCombination))
            {
                Debug.Log("Success!");
                isMatchFound = true;
                // set correct sprite and do all this logic for the book as well
                spriteManager.SetSprite(validCombo.name);
                //TODO update ui text as well
                if (validCombo.isFound == true)
                {
                    AlreadyFound();
                    return;
                }
                //play success sound, particles, etc
                validCombo.isFound = true;
                //activate dialogue relating to the match through dialogue manager instance
                StoryManager.Instance.LaunchStory(validCombo.name);
                LibraryManager.Instance.UpdateSlot(validCombo.name, spriteManager.GetSprite(validCombo.name));
                break;
            }
        }

        if (!isMatchFound)
        {
            Debug.Log("Womp Womp");
            ResSprite(); // later it will be a different function to show a normal cat
            // make a text bubble that shows up with an avatar and says that the DNA got overriden
        }
    }

    public void AlreadyFound()
    {
        Debug.Log("Already found that cat!");
    }

    public void ResSprite()
    {
        spriteManager.ResetSprite();
    }

}
