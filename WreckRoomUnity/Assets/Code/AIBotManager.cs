using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBotManager : MonoBehaviour
{
    public static AIBotManager Instance;
    public List<GameObject> AIBotPrefabs;
    public Transform botSpawnLocation;
    public List<Transform> botDestinations;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnBot()
    {
        int rand = Random.Range(0, AIBotPrefabs.Count);
        Instantiate(AIBotPrefabs[rand], botSpawnLocation.position, botSpawnLocation.rotation, null);
        AIBotPrefabs.Remove(AIBotPrefabs[rand]);
    }
}
