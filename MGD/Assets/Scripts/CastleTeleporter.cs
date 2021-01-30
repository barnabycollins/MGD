using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleTeleporter : MonoBehaviour
{
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(gameController.GetComponent<GameControlScript>().levelLength + 3, transform.position.y);
    }
}
