using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterFX : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 50f;
    public static int charCount;
    public static bool runTextPrint;
    public static TMPro.TMP_Text viewText;
    [SerializeField] string transferText;

    public bool IsRunning {get; private set;}

    //wait time pri znamienkach
    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation (new HashSet<char>(){'.', '!', '?'}, 0.6f),
        new Punctuation (new HashSet<char>(){',', ';', ':'}, 0.3f)
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType, TMP_Text textLabel)
    {
        if (runTextPrint == true)
        {
            runTextPrint = false;
            viewText = GetComponent<TMPro.TMP_Text>();
            transferText = viewText.text;
            viewText.text = "";
        }

        typingCoroutine = StartCoroutine(routine:TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        IsRunning = true;

        textLabel.text = string.Empty;

        float t = 0;
        int charIndex = 0;

        while (charIndex < textToType.Length)
        {
            int lastCharacter = charIndex;

            t += Time.deltaTime * typeSpeed;
            t += Time.deltaTime;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value:charIndex, min:0, max:textToType.Length);

            for (int i = lastCharacter;  i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.text = textToType.Substring(startIndex: 0, length: +1);

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }
            yield return null;
        }
        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctuationCategory in punctuations)
        {
            if(punctuationCategory.Punctuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }
        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
