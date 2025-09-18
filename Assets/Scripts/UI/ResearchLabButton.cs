using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchLabButton : MonoBehaviour
{
    public ResearchNodeSO research;

    public ResearchConnectorUI[] connectors;
    public Button[] nextButton;

    public void DoYourThing()
    {
        Invoke("CheckResearchAndActivateConnectors", 0.1f);
    }
    private void CheckResearchAndActivateConnectors()
    {
        if (research.activated)
        {
            foreach(ResearchConnectorUI c in connectors)
            {
                c.ChangeColor();
                foreach(Button b in nextButton)
                {
                    b.interactable = true;
                }
            }
        }
    }
}
