using System;
using System.Collections;
using System.Collections.Generic;
using GooglePlayGames;
using UnityEngine;
using GooglePlayGames.BasicApi;
public class GooglePlayServicesManager : MonoBehaviour
{

    public static bool isConnectedToGooglePlayServices;
    
    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    private void Start()
    {
        SignInToGooglePlayServices();
    }

    public void SignInToGooglePlayServices()
    {
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
        {
            Debug.Log(result);
            switch (result)
            {
                case SignInStatus.Success:
                    isConnectedToGooglePlayServices = true;
                    break;
                default:
                    isConnectedToGooglePlayServices = false;
                    break;
            }
        });
    }

    public void ReportNewBestScore()
    {
        if (isConnectedToGooglePlayServices)
        {
            Social.ReportScore(PlayerPrefs.GetInt("BestScore"), "BestScore", (success) =>
            {
                if(success) Debug.Log("BestScore reported");
                else Debug.Log("Unable to post highscore");
            } );
        }
        else
        {
            Debug.Log("not signed");
        }
    }
    
}
