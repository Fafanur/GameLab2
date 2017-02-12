using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Load_Manager : MonoBehaviour {
    public GameObject backGround;
    public Slider progressionBar;
    AsyncOperation aSync;

	
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
        }
    }

    IEnumerator LoadProgress(string levelToLoad)
    {
        aSync = SceneManager.LoadSceneAsync(levelToLoad);
        yield return aSync;
    }
}
