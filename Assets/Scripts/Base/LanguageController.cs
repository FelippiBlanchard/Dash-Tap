using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LanguageController : MonoBehaviour
{
    [SerializeField] public List<LanguageText> languageTexts = new List<LanguageText>();
    private void OnEnable()
    {
        var textBox = GetComponent<TextMeshProUGUI>();
        textBox.text = languageTexts[PlayerPrefs.GetInt("Language",0)].text;

    }

    public static void SetLanguage(LanguageText.Language language)
    {
        PlayerPrefs.SetInt("Language", (int)language);
        PlayerPrefs.Save();
    }

}

[System.Serializable]
public class LanguageText{
    public string text;
    public Language language;

    public enum Language { Portugues, Francais, English, Espanol }
}
