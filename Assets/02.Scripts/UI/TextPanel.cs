using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanel : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    public void SetText(string msg)
    {
        _text.text = msg;
    }
}
