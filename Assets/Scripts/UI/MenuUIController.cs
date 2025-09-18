using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    GameObject currentPanel;

    public void ShowPanel(GameObject panel)
    {
        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }
        currentPanel = panel;
        currentPanel.SetActive(true);
    }
    private void Start()
    {
        FindObjectOfType<SoundManager>().SetMusicIndex(0);
    }
}
