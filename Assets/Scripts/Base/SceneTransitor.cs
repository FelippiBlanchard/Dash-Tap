using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;

namespace Base
{
    public class SceneTransitor : MonoBehaviour
    {
        [Header("Settings Transition")]
        [SerializeField] private float timeFading;
        [SerializeField] private float timeMinTransition;
        [SerializeField] private bool havePanelLoading;
        [SerializeField] private GameObject panelLoading;

        [Header("Settings ReloadScene")]
        [SerializeField] private float timeFadingReload;

        [Header("Settings Transition wait Animation")]
        [SerializeField] private float timeToWait;
        [SerializeField] private UnityEvent transitions;

        private void Start()
        {
            if (havePanelLoading)
            {
                var LoadingCG = panelLoading.GetComponent<CanvasGroup>();
                LoadingCG.alpha = 0f;
                panelLoading.SetActive(false);
            }

        }

        public void TransiteWithAnimation(string sceneName)
        {
            StartCoroutine(WaitAnimation(sceneName));
        }
        private IEnumerator WaitAnimation(string sceneName)
        {
            transitions.Invoke();
            yield return new WaitForSeconds(timeToWait);
            SceneManager.LoadScene(sceneName);
        }

        public async void TransiteTo(string sceneName)
        {
            DontDestroyOnLoad(this.gameObject);
            var cg = GetComponent<CanvasGroup>();

            await FadeIn(cg);

            if (havePanelLoading)
            {
                var LoadingCG = panelLoading.GetComponent<CanvasGroup>();
                panelLoading.SetActive(true);
                await FadeIn(LoadingCG,3);
            }

            var load = SceneManager.LoadSceneAsync(sceneName);
            var cont = 0f;
            while (!load.isDone)
            {
                cont += Time.deltaTime;
                await Task.Yield();
            }

            var timeToWait = cont < timeMinTransition ? timeMinTransition - cont : 0f;
            await Task.Delay((int)(timeToWait * 1000));

            if (havePanelLoading)
            {
                var LoadingCG = panelLoading.GetComponent<CanvasGroup>();
                await FadeOut(LoadingCG, 3);
                panelLoading.SetActive(false);
            }


            await FadeOut(cg);
            Destroy(gameObject);
        }

        private async Task FadeIn(CanvasGroup cg, float timeMultiplier = 1f)
        {
            cg.blocksRaycasts = true;

            var currentTimefading = timeFading * timeMultiplier;

            cg.DOFade(1, currentTimefading);
            await Task.Delay((int)(currentTimefading * 1000));
        }
        private async Task FadeOut(CanvasGroup cg, float timeMultiplier = 1f)
        {

            var currentTimefading = timeFading * timeMultiplier;

            cg.DOFade(0, currentTimefading);
            await Task.Delay((int)(currentTimefading * 1000));

            cg.blocksRaycasts = false;
        }

        public async void ExitGame()
        {
            await FadeIn(GetComponent<CanvasGroup>(), 2f);

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        public async void ReloadScene()
        {
            DontDestroyOnLoad(this.gameObject);
            await FadeIn(GetComponent<CanvasGroup>(), timeFadingReload/2);
            var load = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            while (!load.isDone)
            {
                await Task.Yield();
            }
            await FadeOut(GetComponent<CanvasGroup>(), timeFadingReload / 2);
            Destroy(gameObject);
        }

    }

}
