using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScreenShootScript : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private int _widthTexture;
    [SerializeField] private int _heightTexture;

    [SerializeField] List<GameObject> _prefabToScreen;

    private int _index;
    private GameObject _objectSpawn;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CoroutineScreenShot());
    }

    // Update is called once per frame
    void Update() 
    {
        
    }
    
    IEnumerator CoroutineScreenShot ()
    {
        _objectSpawn = Instantiate(_prefabToScreen[_index], Vector3.zero, Quaternion.identity);
        yield return new WaitForEndOfFrame();

        //Creation de la texture
        Texture2D screenShotTexture = new Texture2D(_widthTexture, _heightTexture, TextureFormat.ARGB4444, false);

        //Set render de la camera sur texture
        Rect rectTexture = new Rect(0, 0, _widthTexture, _heightTexture);
        screenShotTexture.ReadPixels(rectTexture, 0, 0);

        //Check si le pixel est vert si oui on le supp
        for(int x = 0; x < _widthTexture;x++ )
        {
            for (int y = 0; y < _heightTexture; y++)
            {
                Color colorPixel = screenShotTexture.GetPixel(x, y);

                if (colorPixel.r <= 0.7 && colorPixel.b <= 0.7 && colorPixel.g > 0.6)
                {
                    Debug.Log("C'est vert je supp");
                    screenShotTexture.SetPixel(x, y, new Color(0,0,0,0));
                }
            }
        }
        
        screenShotTexture.Apply();

        byte[] screenShoteEncode =  screenShotTexture.EncodeToPNG();

        string path = "Assets/ScreenShoot/Screen" + _prefabToScreen[_index].name + ".png";
       
        //Cree la texture en asset
        System.IO.File.WriteAllBytes(path, screenShoteEncode);
        AssetDatabase.Refresh();

        
        ChangeTextureImporter(path);
        CreateScriptable(path);

        _index++;

        Debug.Log("Screen pris et set");

        //Changement de position au cas ou le destroy ce lance apres 2frames
        _objectSpawn.transform.position = Vector3.down * 1000;
        Destroy(_objectSpawn);

        if (_index < _prefabToScreen.Count)
        {
            Debug.Log("Autre screen vas commencer");
            StartCoroutine(CoroutineScreenShot());
        }
        else
        {
            Debug.Log("Tout les screens sont fait");
        }
        
    }

    private void ChangeTextureImporter (string path)
    {
        //Recuper les parametre d'importation
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        //Set parametre d'importation
        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.spritePixelsPerUnit = _widthTexture;
        importer.mipmapEnabled = true;
        importer.isReadable = true;

        //Permet de save les changement d import (comme si cliquer sur apply dans l inspecteur
        importer.SaveAndReimport();
    }

    private void CreateScriptable (string pathTexture)
    {
        //Creation d'un scriptable
        DataCars dataCar = ScriptableObject.CreateInstance<DataCars>();
        dataCar.InitDataCar(_prefabToScreen[_index], (Sprite)AssetDatabase.LoadAssetAtPath(pathTexture, typeof(Sprite)));

        //Creation du scriptable dans les asset
        AssetDatabase.CreateAsset(dataCar, "Assets/Scriptable/DataCarsScriptable/" + _prefabToScreen[_index].name + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
