using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    LoadingScreen ls;
    int i;

    public void LoadScene(int sceneIndex)
    {
        ls = FindObjectOfType<LoadingScreen>();
        i = sceneIndex;
        if (ls != null)
        {
            ls.Unload();
            Invoke("LoadScene", ls.UnloadTime() * 2);
        }
        else
        {
            LoadScene();
        }
        
        
    }
    private void LoadScene()
    {
        SceneManager.LoadScene(i);
    }
    public void ReloadScene()
    {
        
        
        LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
