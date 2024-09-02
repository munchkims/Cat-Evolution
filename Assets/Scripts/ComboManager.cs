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
    [SerializeField] string[] normalCats;

    private SpriteManager spriteManager;
    private bool isMatchFound;
    private bool normalFirstFind = false;
    private List<int> trueAnswers = new List<int>();
    private int hintMeter;

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

                StartCoroutine(SuccessRoutine(validCombo));

                //spriteManager.SetSprite(validCombo.name);
                //catName.text = validCombo.name;
                //textPanel.SetActive(true);
                /* if (validCombo.isFound == true)
                {
                    AlreadyFound();
                    return;
                } */
                //play success sound, particles, etc - this is all through Coroutine
                //validCombo.isFound = true;
                //activate dialogue relating to the match through dialogue manager instance
                //StoryManager.Instance.LaunchStory(validCombo.name);
                //LibraryManager.Instance.UpdateSlot(validCombo.name, spriteManager.GetSprite(validCombo.name));
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
            hintMeter = trueAnswers.Max();
            // we will send different messages to the text bubble and play animation
            /* switch (hintMeter)
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
            } */
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
        AudioManager.Instance.PlayError();
        PopUpController.Instance.ShowBubble("You need to fill all four slots!");
    }

    public void AlreadyFound()
    {
        Debug.Log("Already found that cat!");
    }

    public void NormalCat()
    {
        StartCoroutine(NormalCatRoutine());
        //Debug.Log("Normal cat evoled!");
        /* catName.text = "Ordinary Cat";
        RandomCatSprite();
        textPanel.SetActive(true);
        if (!normalFirstFind)
        {
            normalFirstFind = true;
            StoryManager.Instance.LaunchStory("NormalCat");
        } */
    }

    private void RandomCatSprite()
    {
        int i = Random.Range(0, normalCats.Length);
        spriteManager.SetSprite(normalCats[i]);
    }

    public void ResSprite()
    {
        textPanel.SetActive(false);
        spriteManager.ResetSprite();
    }

    private IEnumerator SuccessRoutine(Combo successCombo)
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        AudioManager.Instance.PlayCombo();
        LiquidController.Instance.UnfillAll();
        yield return new WaitForSeconds(1);
        //shrink down
        BobbleController.Instance.Shrink();
        yield return new WaitForSeconds(1f);
        //change sprite
        spriteManager.SetSprite(successCombo.name);
        //shrink up
        BobbleController.Instance.UnShrink();
        yield return new WaitForSeconds(1f);
        //set name
        catName.text = successCombo.name;
        textPanel.SetActive(true);
        //break here if already found and unlock cursor
        if (successCombo.isFound == true)
        {
            Cursor.lockState = CursorLockMode.None;
            AlreadyFound();
            LiquidController.Instance.FillAll();
            AudioManager.Instance.PlayDefaultCat();
            yield break;
        }
        successCombo.isFound = true;
        //play success sound
        AudioManager.Instance.PlaySuccess();
        //play particles
        //wait for a little bit
        yield return new WaitForSeconds(1f);
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        LiquidController.Instance.FillAll();
        //set off story
        StoryManager.Instance.LaunchStory(successCombo.name);
        yield return new WaitForSeconds(0.5f);
        //update library
        LibraryManager.Instance.UpdateSlot(successCombo.name, spriteManager.GetSprite(successCombo.name));
    }

    private IEnumerator NormalCatRoutine()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        AudioManager.Instance.PlayCombo();
        LiquidController.Instance.UnfillAll();
        yield return new WaitForSeconds(1);
        //shrink down
        BobbleController.Instance.Shrink();
        yield return new WaitForSeconds(1f);
        //change sprite
        RandomCatSprite();
        //shrink up
        BobbleController.Instance.UnShrink();
        yield return new WaitForSeconds(0.5f);
        //set name
        catName.text = "Ordinary Cat";
        textPanel.SetActive(true);
        AudioManager.Instance.PlayDefaultCat();
        //break here if already found and unlock cursor
        if (normalFirstFind)
        {
            AlreadyFound();
            LiquidController.Instance.FillAll();
            ShowHint();
            yield return new WaitForSeconds(1);
            Cursor.lockState = CursorLockMode.None;
            yield break;
        }
        normalFirstFind = true;
        yield return new WaitForSeconds(1f);
        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        LiquidController.Instance.FillAll();
        //set off story
        StoryManager.Instance.LaunchStory("NormalCat");
    }

    private void ShowHint()
    {
        switch (hintMeter)
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
    }

}
