using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image imageField;
    [SerializeField] private Sprite onSprite, offSprite;

    public void UpdateButtonIcon() {
        if (slider.value == 0) {
            imageField.sprite = offSprite;
        } else {
            imageField.sprite = onSprite;
        }
    }
}
