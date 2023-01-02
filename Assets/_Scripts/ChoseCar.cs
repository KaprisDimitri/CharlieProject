using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
#if UNITY_EDITOR
using UnityEditor.Events;
#endif
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

    [SerializeField] private BridgeBetweenScenes _bridgeBetweenScenes;
    [SerializeField] private DataCars _carChose;
    private int _indexCarSelected;

    [SerializeField] private GameObject _socle;
    private GameObject _carSpawned;
    // Start is called before the first frame update
    void Start()
    {
        // teest.onClick.AddListener(tst);
        _bridgeBetweenScenes.CarChose = _carChose;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

#if UNITY_EDITOR
    public void InitScroolBarContent ()
    {

        int numberOfChild = _scroolBarContent.transform.childCount;
        
        for (int i = 0; i< numberOfChild; i++)
        {
            
            DestroyImmediate(_scroolBarContent.transform.GetChild(0).gameObject);
            
        }

        for(int i = 0; i < _carEnable.Count; i++) 
        {
            CreatButton(_carEnable[i],i);
        }

        ChangeCarSelected(0);
    }

    private void CreatButton (DataCars carData,int i)
    {
        //Cree un objet avec le component Button
        GameObject buttonToPut = new GameObject(carData.Name, typeof(Button));
        buttonToPut.transform.parent = _scroolBarContent.transform;

        buttonToPut.AddComponent<Image>();

        buttonToPut.GetComponent<Image>().sprite = carData.ImageCar;
        Button button = buttonToPut.GetComponent<Button>();

        //Ligne de code qui permet de recup une ethode dans un script
        //MethodInfo targetInfo = UnityEvent.GetValidMethodInfo(this, "tst", new System.Type[0]);
        //Debug.Log(targetInfo);

        //Permet de mettre une event en EditMode a un bouton
        ChoseCar script = this;
        //Cree un unity event
        UnityAction<int> action2 = new UnityAction<int>(ChangeCarSelected);
        //Ajouter l event au bouton
        UnityEventTools.AddIntPersistentListener(button.onClick, action2,i);

        
        GameObject imageBackGround = new GameObject("BackGroudButton", typeof(Image));
        imageBackGround.transform.parent = buttonToPut.transform;
        imageBackGround.GetComponent<RectTransform>().localPosition = Vector3.zero;
        imageBackGround.GetComponent<RectTransform>().sizeDelta = new Vector2(200,200);

        GameObject imageCar = new GameObject("ImageCar", typeof(Image));
        imageCar.transform.parent = buttonToPut.transform;
        imageCar.GetComponent<Image>().sprite = carData.ImageCar;
        imageCar.GetComponent<RectTransform>().localPosition = Vector3.zero;
        imageCar.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);
    }
#endif
    public void ChangeCarSelected (int i)
    {
        ResetCarSelected(_indexCarSelected);
        _indexCarSelected = i;
        _carChose = _carEnable[i];
        _bridgeBetweenScenes.CarChose = _carChose;
        _scroolBarContent.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.green;

        _carSpawned = Instantiate(_carChose.CarPrefab, Vector3.zero, Quaternion.identity);
        _carSpawned.transform.parent = _socle.transform;
    }

    private void ResetCarSelected (int i)
    {
        _scroolBarContent.transform.GetChild(i).GetChild(0).GetComponent<Image>().color = Color.white;
        if(_carSpawned == null)
        {
            if(_socle.transform.childCount != 0)
            {
                DestroyImmediate(_socle.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            DestroyImmediate(_carSpawned);
        }
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
            //D:\UnityDosser\UnityProjet\CharlieProject
            int count = Directory.GetFiles(@"D:\UnityDosser\UnityProjet\CharlieProject\Assets\Scriptable\DataCarsScriptable\").Length;
            if (count != 0)
            {
                var filesDataCar = Directory.GetFiles(@"D:\UnityDosser\UnityProjet\CharlieProject\Assets\Scriptable\DataCarsScriptable\");
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
