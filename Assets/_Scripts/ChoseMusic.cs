using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChoseMusic : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _musics;
    [SerializeField] private AudioClip _musicChose;
    private int _indexOfMusic;
    [SerializeField] private BridgeBetweenScenes _bridge;
    [SerializeField] private TextMeshProUGUI _textTiltle;
    // Start is called before the first frame update
    void Start()
    {
        _indexOfMusic = 0;
        _musicChose = _musics[0];
        SetTitle();
        SetInBridge();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic (int indexAdd)
    {
        _indexOfMusic += indexAdd;
        if(_indexOfMusic <0)
        {
            _indexOfMusic = _musics.Count - 1;
        }
        else if (_indexOfMusic >= _musics.Count)
        {
            _indexOfMusic = 0;
        }

        _musicChose = _musics[_indexOfMusic];
        SetTitle();
        SetInBridge();
    }

    private void SetTitle ()
    {
        _textTiltle.text = _musicChose.name;
    }

    private void SetInBridge ()
    {
        _bridge.MusicChose = _musicChose;
    }
}
