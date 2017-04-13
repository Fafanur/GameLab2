using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour {
    public List<string> textList = new List<string>();
    private int currentString;
    private int lastString;
    public float typeSpeed;
    public Text textBar;
    private bool isTyping;
    private bool stopTyping;
    public GameObject textBox;

    void Start()
    {
        lastString = textList.Count - 1;
    }

	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isTyping)
            {
                currentString++;
                if(lastString < currentString)
                {
                    textBox.SetActive(false);
                }
                else
                {
                    StartCoroutine(TypeText(textList[currentString]));
                }
            }
            else if (isTyping && !stopTyping)
            {
                stopTyping = true;
            }
        }
	}

    IEnumerator TypeText (string text)
    {
        int letter = 0;
        textBar.text = "";
        isTyping = true;
        stopTyping = false;
        while (isTyping && !stopTyping && (letter < text.Length - 1)){   
            
            textBar.text += text[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        textBar.text = text;
        isTyping = false;
        stopTyping = false;
    }
}
