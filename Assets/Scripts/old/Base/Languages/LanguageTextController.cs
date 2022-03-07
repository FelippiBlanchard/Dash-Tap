using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Languages
{
    public class LanguageTextController : MonoBehaviour
    {
        [SerializeField] public List<LanguageText> languageTexts = new List<LanguageText>();

        private TextMeshProUGUI textBox;
        private void OnEnable()
        {
            textBox = GetComponent<TextMeshProUGUI>();
            SetText();
        }

        public void SetText()
        {
            textBox.text = languageTexts[PlayerPrefs.GetInt("Language", 0)].text;
        }

        public static void UpdateActiveTexts()
        {
            LanguageTextController[] array = FindObjectsOfType(typeof(LanguageTextController)) as LanguageTextController[];
            if (array != null)
            {
                if (array.Length > 0)
                {
                    foreach (LanguageTextController languageText in array)
                    {
                        languageText.SetText();
                    }
                }
            }
        }

        public static void SetLanguage(LanguageText.Language language)
        {
            PlayerPrefs.SetInt("Language", (int)language);
            PlayerPrefs.Save();
            UpdateActiveTexts();
        }
        public static void SetLanguage(int language)
        {
            PlayerPrefs.SetInt("Language", language);
            PlayerPrefs.Save();
            UpdateActiveTexts();
        }

    }

    [System.Serializable]
    public class LanguageText
    {
        public string text;
        public Language language;

        public enum Language { Portuguese, French, English, Spanish }
    }
}