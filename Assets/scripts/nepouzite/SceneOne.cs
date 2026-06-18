using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.FlowStateWidget;

public class SceneOne : MonoBehaviour
{
    [SerializeField] DummyManager dummyManager;
    [SerializeField] ChoiceManager choiceManager;
    public GameObject dialog_box;
    public GameObject mama;
    [SerializeField] public GameObject sceenManager;

    //[SerializeField] AudioSource #nazov_suboru;

    [SerializeField] public TMP_Text textLabel;
    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLenght;
    [SerializeField] int textLenght;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;

    //zoznam Eventov
    private List<Func<IEnumerator>> dialogueEvents;

    void Start()
    {
        //Zoznam Eventov
        dialogueEvents = new List<Func<IEnumerator>>()
        {
            EventStarter, //0
            EventZero, //1
            EventOne, //2
            EventTwo, //3
            EventThree, //4
            EventFour, //5
            //EventFive //6
        };

        StartCoroutine(EventStarter());
    }

    void Update()
    {
        textLenght = TextCreator.charCount;

        if (choiceManager.isShowingChoices) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextButton();
        }
    }

    public void NextButton()
    {
        if (choiceManager.isShowingChoices) return;

        if (TextCreator.isTyping)
        {
            TextCreator.skipToEnd = true;
            return;
        }

        eventPos++;

        if (eventPos >= dialogueEvents.Count)
        {
            EndDialogue();
        }
        else
        {
            StartCoroutine(dialogueEvents[eventPos]());
        }
    }

    void EndDialogue()
    {
        nextButton.SetActive(false);
        mainTextObject.SetActive(false);
        dialog_box.SetActive(false);
    }

    //Eventy samotné

    IEnumerator EventStarter()
    {
        //eventPos = 0;
        yield return new WaitForSeconds(0.1f);
        //event 0
        nextButton.SetActive(false);

        choiceManager.ShowChoices(
            new string[] { "Talk to them", "Go back" },
            new Action[] {
            () => {eventPos = 2; StartCoroutine(EventOne()); },  // choice 1
            () => StartCoroutine(EventZero())    // choice 2
            }
        );
    }
    IEnumerator EventZero()
    {
        nextButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        dummyManager.ChangeScene(0);
    }

    IEnumerator EventOne()
    {
        //event 1
        yield return new WaitForSeconds(0.2f);
        mainTextObject.SetActive(true);
        nextButton.SetActive(true);
        dialog_box.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "You";
        textToSpeak = "Hello, what is your name and the reason of your visit?";
        dialog_box.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLenght = textToSpeak.Length;
        TextCreator.runTextPrint = true;
    }

    IEnumerator EventTwo()
    {
        yield return new WaitForSeconds(0.2f);
        nextButton.SetActive(true);
        mainTextObject.SetActive(true);
        dialog_box.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Mrs. Sipova";
        textToSpeak = "Good afternoon. I'm Sipova and this is my baby boy Mino. Well... ";
        textToSpeak += "my problem is quite obvious.";
        dialog_box.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLenght = textToSpeak.Length;
        TextCreator.runTextPrint = true;
    }
    IEnumerator EventThree()
    {
        yield return new WaitForSeconds(0.2f);
        nextButton.SetActive(true);
        mainTextObject.SetActive(true);
        dialog_box.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Mrs. Sipova";
        textToSpeak = "Mino is already 7 months old and still doesn't want to get separated. ";
        textToSpeak += "I cannot continue like this, I need to make a living for us!";
        dialog_box.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLenght = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        //yield return new WaitUntil(() => !TextCreator.isTyping);
        if(TextCreator.isTyping == false)
        {
            yield return new WaitForSeconds(1);
            nextButton.SetActive(false);

            choiceManager.ShowChoices(
               new string[] { "Recommend surgery", "Talk to the kid" },
               new Action[] {
               () => {eventPos = 5; StartCoroutine(EventFour()); },  // choice 1
             () => StartCoroutine(EventZero())    // choice 2
               }
             );
        }
    }

    // IEnumerator EventFour()
    //{
    //  yield return new WaitForSeconds(0.2f);
    //nextButton.SetActive(true);
    //mainTextObject.SetActive(true);
    //    dialog_box.SetActive(true);
    //  charName.GetComponent<TMPro.TMP_Text>().text = "Mrs. Sipova";
    //textToSpeak = "I cannot continue like this, I need to make a living for us!";
    //      dialog_box.GetComponent<TMP_Text>().text = textToSpeak;
    //    currentTextLenght = textToSpeak.Length;
    //  TextCreator.runTextPrint = true;

    //yield return new WaitUntil(() => !TextCreator.isTyping);
    //     yield return new WaitForSeconds(0.5f);
    //   nextButton.SetActive(false);

    // choiceManager.ShowChoices(
    //   new string[] { "Recommend surgery", "Talk to the kid" },
    // new Action[] {
    //       () => {eventPos = 2; StartCoroutine(EventFive()); },  // choice 1
    //     () => StartCoroutine(EventZero())    // choice 2
    //   }
    // );
    //}

    IEnumerator EventFour()
    {
        yield return new WaitForSeconds(0.2f);
        nextButton.SetActive(true);
        mainTextObject.SetActive(true);
        dialog_box.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "You";
        textToSpeak = "Hm... I see. ";
        yield return new WaitForSeconds(0.2f);
        textToSpeak += "However, that is outside of my field. You should go to a surgeon.";
        dialog_box.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLenght = textToSpeak.Length;
        TextCreator.runTextPrint = true;
    }
    IEnumerator EventFive()
    {
        yield return new WaitForSeconds(0.2f);
        nextButton.SetActive(true);
        mainTextObject.SetActive(true);
        dialog_box.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Mrs. Sipova";
        textToSpeak = "I cannot continue like this, I need to make a living for us!";
        dialog_box.GetComponent<TMP_Text>().text = textToSpeak;
        currentTextLenght = textToSpeak.Length;
        TextCreator.runTextPrint = true;
    }
}
