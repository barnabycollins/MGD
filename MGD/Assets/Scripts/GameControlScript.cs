using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour
{
    public float levelLength;

    public float levelProgress = 0;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        updateProgress();
    }

    void updateProgress() {
        levelProgress = Mathf.Min(Mathf.Max(0.0f, player.transform.position.x), levelLength) / levelLength;
    }
}
