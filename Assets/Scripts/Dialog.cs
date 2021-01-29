using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject continueButton;
    public TextMeshProUGUI cbtext;
    public string[] sentences;
    public float typingSpeed = 0.2f;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay.text = "";
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        if(textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
        if (index == sentences.Length-1)
            cbtext.text = "PLAY";

    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index])
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        
        if(index < sentences.Length - 1)
        {
            continueButton.SetActive(false);
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            GameManager.gm.LevelSelect("LevelLoader");
        }
    }
}
