using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ComboManager : MonoBehaviour
{
    [SerializeField] List<Combo> validCombinations;
    [SerializeField] MammalClass mammalClass;
    [SerializeField] AnthropodClass anthropodClass;
    [SerializeField] PlantClass plantClass;
    [SerializeField] AlchemyClass alchemyClass;
    [SerializeField] GameObject textPanel;
    [SerializeField] TextMeshProUGUI catName;

    private SpriteManager spriteManager;
    private bool isMatchFound;
    private bool normalFirstFind = false;
    private List<int> trueAnswers = new List<int>();

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

        if (!CheckIfFilled(thisMammal, thisAnthropod, thisPlant, thisElement))
        {
            HandleNotFilled();
            return;
        }

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
                catName.text = validCombo.name;
                textPanel.SetActive(true);
                if (validCombo.isFound == true)
                {
                    AlreadyFound();
                    return;
                }
                //play success sound, particles, etc - this is all through Coroutine
                validCombo.isFound = true;
                //activate dialogue relating to the match through dialogue manager instance
                StoryManager.Instance.LaunchStory(validCombo.name);
                LibraryManager.Instance.UpdateSlot(validCombo.name, spriteManager.GetSprite(validCombo.name));
                break;
            }
            else
            {
                int i = validCombo.combination.CheckForMistakes(thisCombination);
                trueAnswers.Add(i);
            }
        }

        if (!isMatchFound && !normalFirstFind)
        {
            NormalCat();
            return;
        }
        if (!isMatchFound && normalFirstFind)
        {
            Debug.Log("Womp Womp");
            int i = trueAnswers.Max();
            // we will send different messages to the text bubble and play animation
            switch (i)
            {
                case 0:
                    //Debug.Log("I think we are way off...");
                    PopUpController.Instance.ShowBubble("I think we are way off...");
                    break;
                case 1:
                    //Debug.Log("I think one of them might be right");
                    PopUpController.Instance.ShowBubble("I think one of them might be right!");
                    break;
                case 2:
                    //Debug.Log("I'm pretty sure we are halfway there");
                    PopUpController.Instance.ShowBubble("I'm pretty sure we are halfway there!");
                    break;
                case 3:
                    //Debug.Log("Aw man, I was sure we were right! I think we missed just one.");
                    PopUpController.Instance.ShowBubble("Aw man, I was sure we were right! I think we missed just one.");
                    break;
                case 4:
                    Debug.LogWarning("You shouldn't see this code");
                    break;
            }
            trueAnswers.Clear();
            NormalCat(); // later it will be a different function to show a normal cat
            // make a text bubble that shows up with an avatar and says that the DNA got overriden
        }
    }

    private bool CheckIfFilled(Mammals mammal, Anthropods anthropod, Plants plant, Alchemy element)
    {
        if (mammal == Mammals.None || anthropod == Anthropods.None || plant == Plants.None || element == Alchemy.None)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void HandleNotFilled()
    {
        // make text pop up
        PopUpController.Instance.ShowBubble("You need to fill all four slots!");
    }

    public void AlreadyFound()
    {
        Debug.Log("Already found that cat!");
    }

    public void NormalCat()
    {
        Debug.Log("Normal cat evoled!");
        catName.text = "Ordinary Cat";
        textPanel.SetActive(true);
        if (!normalFirstFind)
        {
            normalFirstFind = true;
            StoryManager.Instance.LaunchStory("NormalCat");
        }
    }

    public void ResSprite()
    {
        textPanel.SetActive(false);
        spriteManager.ResetSprite();
    }

}
