using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocleControler : MonoBehaviour
{
    [SerializeField] 
    private float _speedRotation;

    void Update()
    {
        transform.eulerAngles += Vector3.up * _speedRotation * Time.deltaTime;
    }
}
