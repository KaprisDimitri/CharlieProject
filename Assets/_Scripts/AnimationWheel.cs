using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationWheel : MonoBehaviour
{
    private float _speed;
    public float SpeedWheel { set { _speed = value; } }

    private Vector3 _currenteEulerAngle;
    // Start is called before the first frame update
    void Start()
    {
        _currenteEulerAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        _currenteEulerAngle += Vector3.right * Time.deltaTime * _speed;

        transform.eulerAngles = _currenteEulerAngle;
    }
}
