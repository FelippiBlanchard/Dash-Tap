using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "Spawn/GameMode", order = 1)]
public class ScriptableModeGame : ScriptableObject
{
    public MODE mode;

    public void SetKillerMode()
    {
        mode = MODE.Killer;
    }
    public void SetCampaignMode()
    {
        mode = MODE.Campaign;
    }
    public enum MODE { Killer, Campaign}
}
