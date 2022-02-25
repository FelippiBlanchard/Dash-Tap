using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Languages
{
    public class LanguageFirstTime : MonoBehaviour
    {
        [SerializeField] private GameObject modalLanguages;
        private void Start()
        {
            if(PlayerPrefs.GetInt("Language", -1) == -1)
            {
                ShowOptions();
            }
        }
        private void ShowOptions()
        {
            modalLanguages.SetActive(true);
        }
    }
}
