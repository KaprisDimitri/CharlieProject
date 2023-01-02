using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    public Transform SpawnPoint { get { return _spawnPoint; } }
    [SerializeField] private Transform _destroyPoint;
    public Transform DestroyPoint { get { return _destroyPoint; } }

    [SerializeField] private float _speed;
    [SerializeField] List<GameObject> baseTile;
    public List<GameObject> BaseTile { set { baseTile = value; } }

    private Vector3 _dir;

    [SerializeField] private DataTiles _dataTiles;
    public DataTiles GetDataTiles { get { return _dataTiles; } }
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
#if UNITY_EDITOR
    public void DestroyAllBaseTile ()
    {
        for(int i =0; i< baseTile.Count;i++)
        {
            DestroyImmediate(baseTile[i].gameObject);
        }
        baseTile = new List<GameObject>();
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(WorldGenerator))]
public class WorldGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Set Base Tiles"))
        {
            ((WorldGenerator)target).DestroyAllBaseTile();

            int numberOfTileToSpawn = (int)((((WorldGenerator)target).SpawnPoint.position.z - ((WorldGenerator)target).DestroyPoint.position.z)/30);

            List<GameObject> baseTile = new List<GameObject>();

            for(int i = 0; i< numberOfTileToSpawn;i++)
            {
                int randInt = Random.Range(0, ((WorldGenerator)target).GetDataTiles.GetPrefabLenght);
                Vector3 positionToSpawn = new Vector3(((WorldGenerator)target).SpawnPoint.position.x, ((WorldGenerator)target).SpawnPoint.position.y, ((WorldGenerator)target).SpawnPoint.position.z - (30 * i));

                baseTile.Add(Instantiate(((WorldGenerator)target).GetDataTiles.GetTile(randInt), positionToSpawn, Quaternion.identity));
            }

            ((WorldGenerator)target).BaseTile = baseTile;
        }


        base.OnInspectorGUI();
    }
}
#endif


