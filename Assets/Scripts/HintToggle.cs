using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HintToggle : MonoBehaviour
{
    [SerializeField] Sprite offImage;
    [SerializeField] Sprite onImage;
    [SerializeField] GameObject[] hintLines;

    private Button _button;
    private Image _img;
    private bool hintsEnabled = false;

    private void Awake()
    {
        _img = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnToggleButtonClick);
    }

    private void OnToggleButtonClick()
    {
        AudioManager.Instance.PlayUI();
        hintsEnabled = !hintsEnabled;
        _img.sprite = hintsEnabled ? onImage : offImage;

        foreach (GameObject obj in hintLines)
        {
            obj.SetActive(hintsEnabled);
        }

    }
}
