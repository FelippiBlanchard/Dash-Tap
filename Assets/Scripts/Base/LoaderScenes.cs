using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScenes : MonoBehaviour
{
    public void LoadGameScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
