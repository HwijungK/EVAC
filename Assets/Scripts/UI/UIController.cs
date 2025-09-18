using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject buildPanel;
    [HideInInspector] public GameObject inspectPanel;

    [Header("Top Right")]
    public TextMeshProUGUI healthText;
    public Sprite[] healthSprites;
    public Image healthPlanetImage;
    public TextMeshProUGUI materialText;
    public TextMeshProUGUI evacText;

    [Header("BuildPanel")]
    public TextMeshProUGUI[] costTexts;
    public Building[] buildingsInOrder;
    public Button pioneerButton;
    public Button evacShipButton;
    public ResearchNodeSO pioneerResearch;
    public ResearchNodeSO evacShipResearch;

    [Header("End Game")]
    public Image gameOverScreen;
    public TextMeshProUGUI gameOverTMP;
    public Button backToMapButton;
    public Button restartButton;

    private void Awake()
    {
        for (int i = 0; i < costTexts.Length; i++)
        {
            costTexts[i].text = buildingsInOrder[i].buildCost.ToString();
        }
        inspectPanel = null;
    }
    private void Start()
    {
        FindObjectOfType<SoundManager>().SetMusicIndex(0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartRound();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<SceneLoader>().LoadScene(1);
        }
    }

    public void ResetBottomPanel()
    {
        buildPanel.SetActive(false);

        if (inspectPanel != null)
        {
            inspectPanel?.SetActive(false);
        }
        
    }
    public void ShowBuildPanel()
    {
        ResetBottomPanel();
        buildPanel.SetActive(true);

        // check to enable pioneer and evac ship
        switch (pioneerResearch.activated)
        {
            case true: pioneerButton.interactable = true; break;
            default: pioneerButton.interactable = false; break;
        }
        switch (evacShipResearch.activated)
        {
            case true: evacShipButton.interactable = true; break;
            default: evacShipButton.interactable = false; break;
        }
    }
    public void ShowInspectPanel(Building building)
    {
        ResetBottomPanel();
        //print("Enable Inspector");
        building.inspector.gameObject.SetActive(true);
        inspectPanel = building.inspector.gameObject;
        ////inspectPanel.SetActive(true);
    }

    public void OnMainPlanetHealthChange(int newHealth)
    {
        print("health chantged');");
        //healthSpriteIndex++;
        if (5 - newHealth < healthSprites.Length)
        {
            healthPlanetImage.sprite = healthSprites[5-newHealth];
            //print(healthSpriteIndex);
        }
        healthText.text = "" + newHealth;
        if (newHealth <= 0)
        {
            EndGame(false);
        }
    }
    public void OnMaterialAmountChange(int newAmount)
    {
        materialText.text =  "" + newAmount;
    }
    public void OnEvac(int newEvacCount)
    {
        evacText.text = newEvacCount + " / " + EvacCounter.instance.neededEvac;
    }

    public void EndGame(bool win)
    {
        gameOverScreen.gameObject.SetActive(true);
        if (win)
        {
            //print("EndGame");
            gameOverTMP.text = "SUCCESS";
            restartButton.gameObject.SetActive(false);
        }
        else
        {
            gameOverTMP.text = "FAILURE";
            restartButton.gameObject.SetActive(true);
        }
    }
    public void RestartRound()
    {
        FindObjectOfType<SceneLoader>().ReloadScene();
    }
}
