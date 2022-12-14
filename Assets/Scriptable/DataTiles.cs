using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataTile", menuName = "ScriptableObjects/DataTileScriptable", order = 1)]
public class DataTiles : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } }

    [SerializeField] private List<GameObject> _prefabTiles;
    public int GetPrefabLenght { get { return _prefabTiles.Count; } }
    public GameObject GetTile (int i) 
    { 
        if (i < _prefabTiles.Count && i > -1) 
        { 
            return _prefabTiles[i]; 
        } 
        else 
        { 
            return null; 
        } 
    }
}
