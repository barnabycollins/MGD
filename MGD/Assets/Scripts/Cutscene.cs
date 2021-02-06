using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer video = GetComponent<VideoPlayer>();
        video.loopPointReached += SwitchToNextScene;
    }

    void SwitchToNextScene(VideoPlayer vp) {
        vp.targetCameraAlpha = 0;
        StartCoroutine(sceneLoadingCoroutine());
    }

    IEnumerator sceneLoadingCoroutine() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Menu");
    }
}
