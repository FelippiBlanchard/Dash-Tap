using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private float timeToBegin;

    [SerializeField] private List<CharacteristicsSpawn> spawnList;
    [SerializeField] private List<GameObject> prefabList;

    [SerializeField] private List<GameObject> spawners;

    private List<Coroutine> coroutinesSpawner = new List<Coroutine>();

    private bool spawning;


    private void Start()
    {
        //InitializeCharacteristicsSpawn();
    }

    private void Update()
    {
        /*
        if (active)
        {
            Count();
        }
        */
    }


    /*
    private void Count()
    {
        for (int i = 0; i < spawnList.Count; i++)
        {
            spawnList[i].stopwatchSpeed += Time.deltaTime;
            spawnList[i].stopwatchSpawn += Time.deltaTime;
            TryChangeCharacteristics(spawnList[i]);
        }
    }
    */


    /*
    private void InitializeCharacteristicsSpawn()
    {
        foreach (var characteristics in spawnList)
        {
            characteristics.currentCadenceSpawn = characteristics.curren;
            characteristics.currentSpeed = characteristics.variationsSpeed[0].newSpeed;
        }
    }
    */


    private async void Initialize()
    {

        List<Task> tasks = new List<Task>();

        foreach (var spawner in spawnList)
        {

            tasks.Add(VariateSpeed(spawner));
            tasks.Add(VariateCadence(spawner));
        }

        await Task.WhenAll(tasks);


    }



    private async Task VariateSpeed(CharacteristicsSpawn spawner)
    {
        float contador = 0f;
        foreach (var settings in spawner.variationsSpeed.settings)
        {
            while (spawning && contador < settings.timeToVariate)
            {
                contador += Time.deltaTime;
                await Task.Yield();
            }

            spawner.currentSpeed = settings.newValue;

        }
    }

    private async Task VariateCadence(CharacteristicsSpawn spawner)
    {
        float contador = 0f;
        foreach (var settings in spawner.variationsSpeed.settings)
        {
            while (spawning && contador < settings.timeToVariate)
            {
                contador += Time.deltaTime;
                await Task.Yield();
            }

            spawner.currentCadenceSpawn = settings.newValue;

        }
    }


    /*
    public void StartSpawn()
    {
        spawning = true;
        for(int i = 0; i < spawnList.Count; i++)
        {
            coroutinesSpawner.Add(StartCoroutine(SpawnCoroutine(i)));
        }
    }


    */

    private IEnumerator SpawnCoroutine(int index)
    {
        yield return new WaitForSeconds(timeToBegin);

        while (spawning)
        {
            SpawnGameObject(prefabList[index], GetRandomSpawner(), spawnList[index].currentSpeed);
            yield return new WaitForSeconds(spawnList[index].currentCadenceSpawn);
        }

    }



    public void StopSpawn()
    {
        spawning = false;
    }



    private void SpawnGameObject(GameObject prefab, GameObject spawner, float speed)
    {
        var spawned = Instantiate(prefab, spawner.transform);

        SetDirection(spawned, spawner.GetComponent<Spawner>().GetRandomAngle(), speed);

    }



    private void SetDirection(GameObject gameObject, int angle, float speed)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = speed * new Vector2(Mathf.Cos((Mathf.Deg2Rad * angle)), Mathf.Sin((Mathf.Deg2Rad * angle)));
        if (angle < 90 || angle > 270)
        {
            var localscale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector2(-localscale.x, localscale.y);
        }
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
    [SerializeField] public VariationSettings variationsSpeed;

    [Header("Spawn settings")]
    [SerializeField] public VariationSettings variationsCadence;

    /*
    [SerializeField] public float initialSpeed;
    [SerializeField] public float maxSpeed;
    [SerializeField] public float increaserSpeed;
    [SerializeField] public float cadenceSpeed;

    [Header("Spawn settings")]
    [SerializeField] public float initialCadenceSpawn;
    [SerializeField] public float minCadenceSpawn;
    [SerializeField] public float decreaserSpawn;
    [SerializeField] public float cadenceCadenceSpawn;
    */

    [Header("")]
    public float currentSpeed;
    public float currentCadenceSpawn;
    public float stopwatchSpeed;
    public float stopwatchSpawn;

}

[System.Serializable]
public class VariationSettings
{

    [SerializeField] private TypeVariation type;
    [SerializeField] public List<Settings> settings;

    public enum TypeVariation { Speed, Spawn}

}
[System.Serializable]
public class Settings
{
    [SerializeField] public float timeToVariate;
    [SerializeField] public float newValue;
}
