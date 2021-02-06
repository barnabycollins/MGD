using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyUpdater : MonoBehaviour
{
    public GameObject difficultySlider;
    private Slider slider;
    private Text text;

    // Start is called before the first frame update
    void Start()
    {
        slider = difficultySlider.GetComponent<Slider>();
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string toWrite = String.Format("{0:000}%", (int) ((slider.value - 0.01) * 256.411));
        
        if (toWrite == "100 %") {
            toWrite = "PSYCHO";
        }

        text.text = toWrite;
    }
}
