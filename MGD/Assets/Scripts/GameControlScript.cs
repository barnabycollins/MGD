using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControlScript : MonoBehaviour
{
    public float levelLength;

    public float levelProgress;

    public GameObject player;

    public GameObject healthBar;
    private Slider healthSlider;

    public int playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        levelProgress = 0;
        healthSlider = healthBar.GetComponent<Slider>();
        healthSlider.value = healthSlider.maxValue = playerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        updateProgress();
    }

    void updateProgress() {
        levelProgress = Mathf.Min(Mathf.Max(0.0f, player.transform.position.x), levelLength) / levelLength;
    }

    public bool updateHealth(int damage) {
        playerHealth += damage;

        healthSlider.value = playerHealth;

        if (playerHealth <= 0) return false;

        return true;
    }
}
