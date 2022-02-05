using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacteristicSpawn", menuName = "Spawn/CharacteristicsSpawn", order = 1)]
public class ScriptableCharacteristicSpawn : ScriptableObject
{
    [SerializeField] private List<CharacteristicsSpawn> spawnList;
}
