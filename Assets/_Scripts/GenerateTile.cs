using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTile : MonoBehaviour
{
    public delegate void SpawnTile();
    public SpawnTile spawnDeleg;

    private float _speedTile;
    private Transform _destroyPoint;
    private Transform _spawnPoint;
    private Vector3 _dir;

    public void InitTile (SpawnTile delToAdd, float speed, Transform destroyPoint, Transform spawnPoint, Vector3 dir)
    {
        spawnDeleg += delToAdd;
        spawnDeleg += DestroyObj;
        _speedTile = speed;
        _destroyPoint = destroyPoint;
        _spawnPoint = spawnPoint;
        _dir = dir;
    }

    private void Update()
    {
        MoveTile();
        CheckPositionTile();
    }
    
    private void MoveTile ()
    {
        transform.position += _dir * _speedTile * Time.fixedDeltaTime;
    }

    private void CheckPositionTile ()
    {
        if (Vector3.Distance(transform.position, _spawnPoint.position) >= Vector3.Distance(_destroyPoint.position, _spawnPoint.position))
        {
            spawnDeleg.Invoke();
        }
    }

    private void DestroyObj ()
    {
        Destroy(gameObject);
    }
}
