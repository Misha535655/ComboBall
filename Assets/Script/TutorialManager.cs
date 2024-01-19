using UnityEngine;
using System.Collections;
using System;

public class TutorialManager : MonoBehaviour
{
    public GameObject centerPanel;
    public GameObject[] panel;
    private int panelCount = 0;
    private string isFirst = "true";



    void Start()
    {
        PlayerPrefs.SetString("isFirst", "true");
        PlayerPrefs.Save();
        LoadScore();
        Debug.Log(isFirst);
        if(isFirst == "true")
        {
            HideAllPanels();
            ShowPanel(centerPanel);
            ShowPanel(panel[panelCount]);
            panelCount = 1;
            Time.timeScale = 0f;
        }
        else
        {
            Destroy(gameObject);
        }
       
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Создаем новую панель, если не превышено максимальное количество
            if (panelCount < 3)
            {
                ShowPanel(panel[panelCount]);
                HidePanel(panel[panelCount - 1]);
                panelCount++;
            }
            else
            {
                HideAllPanels();
                isFirst = "false";
                SaveScore();
                Destroy(gameObject);
            }
        }
    }
    void SaveScore()
    {
        PlayerPrefs.SetString("isFirst", isFirst);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        if (PlayerPrefs.HasKey("isFirst"))
        {
            isFirst = PlayerPrefs.GetString("isFirst");
        }
    }

    void ShowPanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
    }

    void HidePanel(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    void HideAllPanels()
    {
        foreach(GameObject obj in panel)
        {
            obj.SetActive(false);
        }
    }
}