using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private GameObject sliderObject;
    [SerializeField] private Image imageField;
    [SerializeField] private Sprite onSprite, offSprite;

    private Slider slider;

    private void Start() {
        slider = sliderObject.GetComponent<Slider>();
    }

    public void UpdateButtonIcon() {
        if (slider.value == 0) {
            imageField.sprite = offSprite;
        } else {
            imageField.sprite = onSprite;
        }
    }

    public void ToggleSlider() {
        sliderObject.SetActive(!sliderObject.activeSelf);
    }
}
