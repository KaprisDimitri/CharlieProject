using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tst : MonoBehaviour
{
    [SerializeField] private LayerMask _layerTest;
    [SerializeField] private LayerMask _layerValue;
    [SerializeField] private GameObject _tstObj;
    [SerializeField] private GameObject _tstObjSecond;

    private PremiereEnumFlag _enumFlags;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(_layerValue.value);
        // Debug.Log(_layerTest.value);

        //Left Shift est pour decaler dans les bit la valeur pour remettre a niveau (le LAYER de unity ne commence pas par
        // 0 comme une enum flag normal elle commence par 1 on dois donc enlever 1byte pour s alligner au layer de unity)

        /*Debug.Log((_layerTest.value & (1 << _tstObj.layer)) + " & " + (int)_tstObj.layer);
        Debug.Log((_layerTest.value & (1 << _tstObjSecond.layer)) + " & " + _tstObjSecond.layer);
        Debug.Log((_layerTest.value | (1 << _tstObj.layer)) + " | " + _tstObj.layer);
        Debug.Log((_layerTest.value | (1 << _tstObjSecond.layer)) + " | " + _tstObjSecond.layer);*/

        _enumFlags = PremiereEnumFlag.troisieme | PremiereEnumFlag.quatrieme;

        int valeur  = (int)(_enumFlags & (PremiereEnumFlag.troisieme));
        int valeur2 = (int)(_enumFlags & (PremiereEnumFlag.sieptieme));
        int valeur3 = (int)(_enumFlags & (PremiereEnumFlag.cinquieme));
        int valeur4 = (int)(_enumFlags & (PremiereEnumFlag.troisieme | PremiereEnumFlag.sieptieme));
        Debug.Log(_enumFlags & (PremiereEnumFlag.troisieme));
        Debug.Log(_enumFlags & (PremiereEnumFlag.sieptieme));
        Debug.Log((int)_enumFlags);
        Debug.Log(valeur);
        Debug.Log(valeur2);
        Debug.Log(valeur3);
        Debug.Log(valeur4);
        //Debug.Log(_enumFlags & (1 << PremiereEnumFlag.sieptieme));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Flags]
    private enum PremiereEnumFlag
    {
        premier = 0,
        deuxieme = 1,
        troisieme = 2,
        quatrieme = 4,
        cinquieme = 8,
        sixieme = 16,
        sieptieme = 32,
        huitieme = 64,
    }
}
