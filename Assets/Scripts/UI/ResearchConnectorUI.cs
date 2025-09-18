using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResearchConnectorUI : MonoBehaviour
{
    public Color enabledColor;

    public void ChangeColor()
    {
        GetComponent<Image>().color = enabledColor;
    }
}
