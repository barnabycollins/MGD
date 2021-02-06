using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public GameObject difficultySlider;

    static public float difficulty = 0.205f;

    void Start() {
        difficultySlider.GetComponent<Slider>().value = difficulty;
    }

    public void StartGame() {
        difficulty = difficultySlider.GetComponent<Slider>().value;
        SceneManager.LoadSceneAsync("MainScene");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
