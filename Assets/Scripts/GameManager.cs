using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject tailPrefab;
    public GameObject collectiblePrefab;
    public TransformCollector lastCollector;
    public Material collectibleMat;
    public Material boundaryMat;
    public int collectibleRadius = 10;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    void Start()
    {
        SpawnACollectible();
        AddATail();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        collectibleMat.mainTextureOffset = new Vector2(0, Time.timeSinceLevelLoad);
        boundaryMat.mainTextureOffset = new Vector2(0, -Time.timeSinceLevelLoad / 2);
    }

    void OnDisable()
    {
        collectibleMat.mainTextureOffset = Vector2.zero;
        boundaryMat.mainTextureOffset = Vector2.zero;
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void SpawnACollectible()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-1f, 1f) * collectibleRadius, 1, Random.Range(-1f, 1f) * collectibleRadius);
        Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
    }

    public void AddATail()
    {
        GameObject spawnedTail = Instantiate(tailPrefab, lastCollector.transform.position, tailPrefab.transform.rotation);
        spawnedTail.GetComponent<TransformUpdater>().collector = lastCollector;
        lastCollector = spawnedTail.GetComponent<TransformCollector>();
        spawnedTail.SetActive(true);
    }
}
