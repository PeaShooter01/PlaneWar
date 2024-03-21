using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text : MonoBehaviour
{
    public TMP_Text tmp;
    public void setText(string text)
    {
        tmp.text = text;
    }
}
