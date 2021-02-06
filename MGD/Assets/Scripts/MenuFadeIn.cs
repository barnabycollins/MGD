using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFadeIn : MonoBehaviour
{
    public float fadeTime;

    private float fadeMult;

    private Image image;
    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        startTime = Time.time;
        fadeMult = 1/fadeTime;
    }

    // Update is called once per frame
    void Update()
    {
        float elapsed = Time.time - startTime;
        image.color = new Color(0, 0, 0, Mathf.Max(1 - elapsed*fadeMult, 0.0f));
        if (elapsed > fadeTime) {
            gameObject.SetActive(false);
        }
    }
}
