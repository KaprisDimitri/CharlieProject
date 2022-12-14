using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCars : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name { get { return _name; } }

    [SerializeField] private GameObject _carPrefab;
    public GameObject CarPrefab { get { return _carPrefab; } }

    [SerializeField] private Sprite _imageCar;
    public Sprite ImageCar { get { return _imageCar; } }

    public void InitDataCar (GameObject carPrefab, Sprite imageCar)
    {
        _carPrefab = carPrefab;
        _imageCar = imageCar;
        _name = carPrefab.name;
    }
}
