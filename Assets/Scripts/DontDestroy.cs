using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private AudioSource audioS;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += onSceneLoaded;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioS.mute = false;
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }
}
