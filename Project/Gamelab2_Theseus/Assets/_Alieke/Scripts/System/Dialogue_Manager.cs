using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue_Manager : MonoBehaviour {
    public List<string> textList = new List<string>();
    int currentString;
    public Text textBar;

    void Awake()
    {
        SetText();
    }

	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            currentString++;
            SetText();
        }
	}

    void SetText()
    {
        textList[currentString] = textList[currentString].Replace("@", System.Environment.NewLine);
        textBar.text = textList[currentString];
    }
}
