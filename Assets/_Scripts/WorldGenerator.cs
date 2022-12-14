using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _destroyPoint;
    [SerializeField] private float _speed;
    [SerializeField] List<GameObject> baseTile;

    private Vector3 _dir;

    [SerializeField] private DataTiles _dataTiles;
    // Start is called before the first frame update
    void Start()
    {
        _dir = _destroyPoint.position - _spawnPoint.position;

        for(int i = 0;i<baseTile.Count;i++)
        {
            ApplyScriptToTile(baseTile[i]);
        }

        SpawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnTile ()
    {
        int randInt = Random.Range(0, _dataTiles.GetPrefabLenght);

        GameObject tileSpawn = Instantiate(_dataTiles.GetTile(randInt), _spawnPoint.transform.position, Quaternion.identity);
        ApplyScriptToTile(tileSpawn);

    }

    private void ApplyScriptToTile (GameObject tile)
    {
        GenerateTile generateTile = tile.AddComponent<GenerateTile>();
        generateTile.InitTile(new GenerateTile.SpawnTile(SpawnTile), _speed, _destroyPoint, _spawnPoint, _dir);
    }
}


