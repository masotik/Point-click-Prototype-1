using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    [SerializeField] GameObject choiceContainer1;
    [SerializeField] GameObject choiceContainer2;
    [SerializeField] GameObject choiceContainer3;
    [SerializeField] TMP_Text choiceLabel1;
    [SerializeField] TMP_Text choiceLabel2;
    [SerializeField] TMP_Text choiceLabel3;

    private GameObject[] containers;
    private TMP_Text[] labels;
    public bool isShowingChoices = false;

    void Start()
    {
        containers = new GameObject[] { choiceContainer1, choiceContainer2, choiceContainer3 };
        labels = new TMP_Text[] { choiceLabel1, choiceLabel2, choiceLabel3 };
        HideChoices();
    }

    public void ShowChoices(string[] choiceTexts, Action[] callbacks)
    {
        HideChoices();
        isShowingChoices = true;

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            if (i >= containers.Length) break;

            containers[i].SetActive(true);
            labels[i].text = choiceTexts[i];

            int index = i;
            Button btn = containers[i].GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() =>
            {
                HideChoices();
                callbacks[index]();
            });
        }
    }

    public void HideChoices()
    {
        isShowingChoices = false;
        foreach (GameObject container in containers)
            if (container != null)
                container.SetActive(false);
    }
}



