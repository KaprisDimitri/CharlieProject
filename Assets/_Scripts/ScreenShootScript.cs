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
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CoroutineScreenShot());
    }

    // Update is called once per frame
    void Update() 
    {
        
    }
    
    IEnumerator CoroutineScreenShot ()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenShotTexture = new Texture2D(_widthTexture, _heightTexture, TextureFormat.ARGB4444, false);
        Rect rectTexture = new Rect(0, 0, _widthTexture, _heightTexture);
        screenShotTexture.ReadPixels(rectTexture, 0, 0);

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

        string path = "Assets/ScreenShoot/" + _prefabToScreen[_index].name + ".png";
       
        System.IO.File.WriteAllBytes(path, screenShoteEncode);
        AssetDatabase.Refresh();

        
        ChangeTextureImporter(path);
        CreateScriptable(path);
       

        Debug.Log("j ai fait mon screen gros");
    }

    private void ChangeTextureImporter (string path)
    {
        TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

        importer.textureType = TextureImporterType.Sprite;
        importer.spriteImportMode = SpriteImportMode.Single;
        importer.spritePixelsPerUnit = _widthTexture;
        importer.mipmapEnabled = true;
        importer.isReadable = true;

        importer.SaveAndReimport();
    }

    private void CreateScriptable (string pathTexture)
    {
        DataCars dataCar = ScriptableObject.CreateInstance<DataCars>();
        dataCar.InitDataCar(_prefabToScreen[_index], (Sprite)AssetDatabase.LoadAssetAtPath(pathTexture, typeof(Sprite)));
        AssetDatabase.CreateAsset(dataCar, "Assets/Scriptable/DataCarsScriptable/" + _prefabToScreen[_index].name + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
