using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float timeToBegin;

    [SerializeField] private List<CharacteristicsSpawn> spawnList;
    [SerializeField] private List<GameObject> prefabList;

    [SerializeField] private List<GameObject> spawners;

    private List<Coroutine> coroutinesSpawner = new List<Coroutine>();

    private bool active;


    private void Start()
    {
        InitializeCharacteristicsSpawn();
    }

    private void Update()
    {
        if (active)
        {
            Count();
        }
    }


    private void Count()
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            spawnList[i].stopwatchSpeed += Time.deltaTime;
            spawnList[i].stopwatchSpawn += Time.deltaTime;
            TryChangeCharacteristics(spawnList[i]);
        }
    }


    private void InitializeCharacteristicsSpawn()
    {
        foreach (var characteristics in spawnList)
        {
            characteristics.currentCadenceSpawn = characteristics.initialCadenceSpawn;
            characteristics.currentSpeed = characteristics.initialSpeed;
        }
    }



    private void TryChangeCharacteristics(CharacteristicsSpawn charSpawn)
    {
        if(charSpawn.stopwatchSpawn >= charSpawn.cadenceCadenceSpawn)
        {
            if (charSpawn.currentCadenceSpawn >= charSpawn.minCadenceSpawn)
            {
                charSpawn.stopwatchSpawn = 0;
                charSpawn.currentCadenceSpawn -= charSpawn.decreaserSpawn;
            }
            else
            {
                charSpawn.currentCadenceSpawn = charSpawn.minCadenceSpawn;
            }
        }
        if(charSpawn.stopwatchSpeed >= charSpawn.cadenceSpeed)
        {
            if(charSpawn.currentSpeed <= charSpawn.maxSpeed)
            {
                charSpawn.stopwatchSpeed = 0;
                charSpawn.currentSpeed += charSpawn.increaserSpeed;
            }
            else{
                charSpawn.currentSpeed = charSpawn.maxSpeed;
            }
        }
    }



    public void StartSpawn()
    {
        active = true;
        for(int i = 0; i < spawnList.Count; i++)
        {
            coroutinesSpawner.Add(StartCoroutine(SpawnCoroutine(i)));
        }
    }



    private IEnumerator SpawnCoroutine(int index)
    {
        yield return new WaitForSeconds(timeToBegin);

        while (true)
        {
            SpawnGameObject(prefabList[index], GetRandomSpawner(), spawnList[index].currentSpeed);
            yield return new WaitForSeconds(spawnList[index].currentCadenceSpawn);
        }

    }



    public void StopSpawn()
    {
        StopAllCoroutines();
        active = false;
    }



    private void SpawnGameObject(GameObject prefab, GameObject spawner, float speed)
    {
        var attacker = Instantiate(prefab, spawner.transform);

        SetDirection(attacker, spawner.GetComponent<Spawner>().GetRandomAngle(), speed);

    }



    private void SetDirection(GameObject gameObject, int angle, float speed)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos((Mathf.Deg2Rad * angle)), Mathf.Sin((Mathf.Deg2Rad * angle)));
    }



    private GameObject GetRandomSpawner()
    {
        int index = Random.Range(0, spawners.Count);
        return spawners[index];
    }



}

[System.Serializable]
public class CharacteristicsSpawn
{
    [SerializeField] private string name;

    [Header("Speed settings")]
    [SerializeField] public float initialSpeed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float increaserSpeed;
    [SerializeField] public float cadenceSpeed;

    [Header("Spawn settings")]
    [SerializeField] public float initialCadenceSpawn;
    [SerializeField] public float minCadenceSpawn;
    [SerializeField] public float decreaserSpawn;
    [SerializeField] public float cadenceCadenceSpawn;

    [Header("")]
    public float currentSpeed;
    public float currentCadenceSpawn;
    public float stopwatchSpeed;
    public float stopwatchSpawn;

}
