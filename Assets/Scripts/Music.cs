using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour{
    private AudioSource _audioSource;

    [SerializeField] List<AudioClip> _audioClipsList;

    private int indexOfMusic = 0;

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
        Shuffle(_audioClipsList);
        _audioSource.PlayOneShot(_audioClipsList[indexOfMusic]);
    }

    private void Update(){
        if (_audioSource.isPlaying){
            return;
        }

        indexOfMusic++;
        if (indexOfMusic >= _audioClipsList.Count){
            Shuffle(_audioClipsList);
            indexOfMusic = 0;
            _audioSource.PlayOneShot(_audioClipsList[indexOfMusic]);
        }
        else{
            _audioSource.PlayOneShot(_audioClipsList[indexOfMusic]);
        }
    }

    public void Shuffle(List<AudioClip> audioClipsList){
        var count = audioClipsList.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i){
            var r = UnityEngine.Random.Range(i, count);
            var tmp = audioClipsList[i];
            audioClipsList[i] = audioClipsList[r];
            audioClipsList[r] = tmp;
        }
    }
}