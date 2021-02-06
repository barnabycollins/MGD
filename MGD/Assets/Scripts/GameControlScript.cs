using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour
{
    public float levelLength;

    public float levelProgress;

    public GameObject player;

    public GameObject healthBar;
    private Slider healthSlider;

    public GameObject weaponBar;
    private Slider weaponSlider;

    public GameObject distanceBar;
    private Slider distanceSlider;

    public int playerHealth;

    public string gameState;

    public GameObject endPanel;
    public GameObject endMessage;
    public GameObject topStatNameText;
    public GameObject topStatValueText;
    public GameObject[] statsTexts;

    // Start is called before the first frame update
    void Start()
    {
        levelProgress = 0;

        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.value = healthSlider.maxValue = playerHealth;

        weaponSlider = weaponBar.GetComponent<Slider>();
        weaponSlider.value = 1;

        distanceSlider = distanceBar.GetComponent<Slider>();
        distanceSlider.value = 0;

        gameState = "running";
    }

    // Update is called once per frame
    void Update()
    {
        updateProgress();
    }

    void updateProgress() {
        levelProgress = Mathf.Min(Mathf.Max(0.0f, player.transform.position.x / levelLength), 1.0f);

        distanceSlider.value = levelProgress;
    }

    public bool updateHealth(int damage) {
        playerHealth += damage;

        playerHealth = Mathf.Max(0, playerHealth);

        healthSlider.value = playerHealth;

        if (playerHealth == 0) return false;

        return true;
    }

    public void updateFireCooldown(float proportionOfTime) {
        weaponSlider.value = proportionOfTime;
    }

    public void end(bool won, IDictionary<string, float> stats) {
        string message;

        string topStatName;
        float topStatValue;

        if (won) {
            gameState = "win";
            message = "YOU WIN!";
            topStatName = "Health left:";
            topStatValue = 100.0f - playerHealth / 10;
        }
        else {
            gameState = "lose";
            message = "YOU LOSE!";
            topStatName = "Distance left:";
            topStatValue = 100.0f - levelProgress * 100;
        }

        endMessage.GetComponent<Text>().text = message;
        topStatNameText.GetComponent<Text>().text = topStatName;
        topStatValueText.GetComponent<Text>().text = String.Format("{0:00.0} %", topStatValue);
        
        string[] statsNames = new string[] {"timeTaken", "weaponFires", "enemiesKilled"};

        for (int i = 0; i < statsNames.Length; i++) {
            string statName = statsNames[i];

            string toWrite;

            if (statName == "timeTaken") {
                toWrite = String.Format("{0:0.00} s", stats[statName]);
            }
            else {
                toWrite = ((int) stats[statName]).ToString();
            }

            statsTexts[i].GetComponent<Text>().text = toWrite;
        }

        endPanel.SetActive(true);
    }

    public void goToMenu() {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void resetScene() {
        SceneManager.LoadSceneAsync("MainScene");
    }
}
