using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public Slider slider;
    private Text text;
    public string preMess;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        float mess = slider.value;
        text.text = preMess + ": " + mess.ToString();
    }
}
