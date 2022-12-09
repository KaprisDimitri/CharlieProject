using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChoseCar : MonoBehaviour
{
    [SerializeField] private List<DataCars> _carEnable;
    public List<DataCars> CarEnable
    {
        set { _carEnable = value; }
        get { return _carEnable; }
    }

    [SerializeField] private GameObject _scroolBarContent;

    [SerializeField] private DataCars _carChose;

    [SerializeField] private Button teest;

    // Start is called before the first frame update
    void Start()
    {
       // teest.onClick.AddListener(tst);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitScroolBarContent ()
    {
        for(int i = 0; i < _carEnable.Count; i++) 
        {
            CreatButton(_carEnable[i],i);
        }
    }

    private void CreatButton (DataCars carData,int i)
    {
        GameObject buttonToPut = new GameObject(carData.Name, typeof(Button));
        buttonToPut.transform.parent = _scroolBarContent.transform;

        buttonToPut.AddComponent<Image>();

        buttonToPut.GetComponent<Image>().sprite = carData.ImageCar;
        //buttonToPut.GetComponent<Button>().onClick.
        MethodInfo targetInfo = UnityEvent.GetValidMethodInfo(buttonToPut.GetComponent<Button>().onClick, "tst", new System.Type[0]);
        Debug.Log(targetInfo);
        UnityAction methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction), buttonToPut.GetComponent<Button>(), targetInfo) as UnityAction;
        UnityEventTools.AddPersistentListener(buttonToPut.GetComponent<Button>().onClick, methodDelegate);

        
    }

    public void tst (int i)
    {
        Debug.Log("aaaa");
    }

    public void ChangeCarSelected (int i)
    {
        _carChose = _carEnable[i];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ChoseCar))]
public class EditorChoseCar : Editor
{
    private void OnEnable()
    {
        
    }
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Check And Add DataCars"))
        {
            int count = Directory.GetFiles(@"C:\Users\Dim\CharlieProject\Assets\Scriptable\DataCarsScriptable\").Length;
            if (count != 0)
            {
                var filesDataCar = Directory.GetFiles(@"C:\Users\Dim\CharlieProject\Assets\Scriptable\DataCarsScriptable\");
                List<DataCars> _carEnable = new List<DataCars>();
                for (int i = 0; i < count; i++)
                {
                    var fileDataCar = filesDataCar[i];
                    if (Path.GetExtension(fileDataCar) != ".meta")
                    {
                        _carEnable.Add((DataCars)AssetDatabase.LoadAssetAtPath("Assets/Scriptable/DataCarsScriptable/" + Path.GetFileName(fileDataCar), typeof(DataCars)));
                    }
                }
                ((ChoseCar)target).CarEnable = _carEnable;
            }
            else
            {
                Debug.Log("Aucun fichier n'est dans le dossier");
            }
        }
        if (GUILayout.Button("Set Cars In UI"))
        {
            ((ChoseCar)target).InitScroolBarContent();
        }
        base.OnInspectorGUI();
    }
}
#endif
