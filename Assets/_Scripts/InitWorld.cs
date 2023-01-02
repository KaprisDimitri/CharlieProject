using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitWorld : MonoBehaviour
{
    [SerializeField] private BridgeBetweenScenes _bridgeBetweenScenes;
    [SerializeField] private float _speedwheel;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private StartRun _startRun;
    // Start is called before the first frame update
    void Start()
    {
        SetCar();
        SetMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if(!_audioSource.isPlaying)
        {
            _startRun.LoadScene(0);
        }
    }

    private void SetCar ()
    {
        GameObject carSpawn = Instantiate(_bridgeBetweenScenes.CarChose.CarPrefab, Vector3.zero, Quaternion.identity);

        for (int i = 1; i < carSpawn.transform.childCount; i++)
        {
            AnimationWheel animWheel = carSpawn.transform.GetChild(i).gameObject.AddComponent<AnimationWheel>();
            animWheel.SpeedWheel = _speedwheel;
        }
    }

    private void SetMusic ()
    {
        _audioSource.clip = _bridgeBetweenScenes.MusicChose;
        _audioSource.Play();
    }
}
