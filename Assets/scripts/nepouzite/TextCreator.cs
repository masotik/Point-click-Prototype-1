using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCreator : MonoBehaviour
{
    public static TMPro.TMP_Text viewText;
    public static bool runTextPrint;
    public static int charCount;
    public string textToType;
    [SerializeField] string transferText;
    [SerializeField] int internalCount;
    public static bool skipToEnd;
    public static bool isTyping;

    //    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    //    {
    //        new Punctuation (new HashSet<char>(){'.', '!', '?'}, 1),
    //        new Punctuation (new HashSet<char>(){',', ';', ':'}, 0.5f)
    //    };

    void Update()
    {
        internalCount = charCount;
        charCount = GetComponent<TMPro.TMP_Text>().text.Length;
        if (runTextPrint == true)
        {
            runTextPrint = false;
            viewText = GetComponent<TMPro.TMP_Text>();
            transferText = viewText.text;
            viewText.text = "";
            StartCoroutine(RollText());
        }
    }

    IEnumerator RollText()
    {
        isTyping = true;
        foreach (char c in transferText)
        {
            if (skipToEnd)
            {
                skipToEnd = false;
                isTyping = false;
                viewText.text = transferText;
                yield break;                 
            }

            viewText.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        isTyping = false;
    }




//    private bool IsPunctuation(char character, out float waitTime)
//    {
//        foreach (Punctuation punctuationCategory in punctuations)
//        {
//            if (punctuationCategory.Punctuations.Contains(character))
//            {
//                waitTime = punctuationCategory.WaitTime;
//                return true;}
//        }
//        waitTime = default;
//        return false;
//    }

//    private readonly struct Punctuation
//    {
//        public readonly HashSet<char> Punctuations;
//        public readonly float WaitTime;

//        public Punctuation(HashSet<char> punctuations, float waitTime)
//        {
//            Punctuations = punctuations;
//            WaitTime = waitTime;
//        }
//    }
}