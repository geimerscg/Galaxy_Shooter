using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] _powerUps;

    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        //spawn game objects every five seconds
        //create a coroutine of type Ienumerator and yield events for 5 sec
        //use a while loop
    }

    IEnumerator SpawnEnemyRoutine()
    {
        //use a while loop
        //instantiate an enemy prefab
        //yeild for 5 secs

        while (_stopSpawning == false)
        {
            Vector3 startingSpot = new Vector3(Random.Range(-9.5f, 9.5f), 6.5f, 0);
            GameObject newEnemey =  Instantiate(_enemyPrefab, startingSpot, Quaternion.identity);
            newEnemey.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 startingSpot = new Vector3(Random.Range(-9.5f, 9.5f), 6.5f, 0);
            GameObject newPowerUp = Instantiate(_powerUps[Random.Range(0,3)], startingSpot, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 7));
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
