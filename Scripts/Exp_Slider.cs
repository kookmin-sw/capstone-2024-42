using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Exp_Slider : MonoBehaviour
{
    public Slider ExpSlider;
    public TMP_Text Text_Slider;

    public void ChangeSlider()
    {
        Text_Slider.text = ExpSlider.value.ToString();
    }
}
