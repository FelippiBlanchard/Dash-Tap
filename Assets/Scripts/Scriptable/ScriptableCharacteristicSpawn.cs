using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacteristicSpawn", menuName = "Spawn/CharacteristicsSpawn", order = 1)]
public class ScriptableCharacteristicSpawn : ScriptableObject
{
    public List<CharacteristicsSpawn> spawnList;
}
