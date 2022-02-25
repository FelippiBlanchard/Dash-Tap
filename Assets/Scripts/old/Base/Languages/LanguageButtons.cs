using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Languages
{
    public class LanguageButtons : MonoBehaviour
    {
        [SerializeField] private List<Image> imageButtons;

        [SerializeField] private Color desactive;
        [SerializeField] private Color active;


        private void OnEnable()
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            int languageIndex = PlayerPrefs.GetInt("Language", 0);
            for (int i = 0; i < imageButtons.Count; i++)
            {
                imageButtons[i].color = i == languageIndex ? active : desactive;
            }
        }

        public void SetLanguage(int language)
        {
            LanguageTextController.SetLanguage(language);
            UpdateButtons();
        }

    }
}
