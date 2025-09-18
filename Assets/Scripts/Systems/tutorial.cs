using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class tutorial : MonoBehaviour
{
    public Transform tutorialObject;
    public Image spotlight;
    public Vector3[] spotlightPosAndSize;
    public Transform texts;
    public int pointerOverNoZoneCount;
    //public Canvas canvas;

    int tutorialIndex = 0;

    private void Start()
    {
        ShowSlide();
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && pointerOverNoZoneCount == 0) {
            tutorialIndex++;
            Invoke("ShowSlide", 0.1f);
        }
        tutorialObject.SetSiblingIndex(tutorialObject.parent.childCount-1);
    }
    void ShowSlide()
    {
        if (tutorialIndex < texts.childCount)
        {
            spotlight.rectTransform.localPosition = (Vector2)spotlightPosAndSize[tutorialIndex];
            spotlight.rectTransform.localScale = Vector3.one * spotlightPosAndSize[tutorialIndex].z;
            try
            {
                texts.GetChild(tutorialIndex - 1).gameObject.SetActive(false);
            }
            catch { }
            texts.GetChild(tutorialIndex).gameObject.SetActive(true);
        }
        else
        {
            tutorialObject.gameObject.SetActive(false);
        }
    }
    public void PointerEnterNoZone()
    {
        pointerOverNoZoneCount++;
    }
    public void PointerExitNoZone()
    {
        pointerOverNoZoneCount--;
    }
}
