using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load_Manager : MonoBehaviour {
    public GameObject backGround;
    public Slider progressionBar;
    AsyncOperation aSync;

    public GameObject canStartText;

	public void LoadScene (string levelToLoad) {
        backGround.SetActive(true);
        progressionBar.gameObject.SetActive(true);
        StartCoroutine(LoadProgress(levelToLoad));
	}

    public void Update()
    {
        if(aSync != null)
        {
            progressionBar.value = aSync.progress;
            if (aSync.progress == 0.9f)
            {
                progressionBar.value = 1;
                canStartText.SetActive(true);
                aSync.allowSceneActivation = false;   
                if(Input.GetButton("Open Stats"))
                {
                    aSync.allowSceneActivation = true;
                }
            }
        }
    }

    IEnumerator LoadProgress(string levelToLoad)
    {
        aSync = SceneManager.LoadSceneAsync(levelToLoad);
        yield return aSync;
    }
}
