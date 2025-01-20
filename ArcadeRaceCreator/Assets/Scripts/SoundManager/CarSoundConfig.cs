using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CarSoundConfig : SoundConfig {
    private const string MOVE = "https://s3.eponesh.com/games/files/13071/animal-walking-on-grass3.mp3";
    private const string STRIKE = "https://s3.eponesh.com/games/files/13071/sfx-strike1.mp3";
    private const string DRIFTSTART = "https://s3.eponesh.com/games/files/13071/sfx-jump1.mp3";
    private const string DRIFTED = "https://s3.eponesh.com/games/files/13071/sfx-end-jump.mp3";

    private List<string> _clipUrl = new List<string>() { MOVE, STRIKE, DRIFTSTART, DRIFTED };

    public IReadOnlyList<string> ClipUrl => _clipUrl;

    [field: SerializeField] public AudioClip Move { get; private set; }
    [field: SerializeField] public AudioClip Strike { get; private set; }
    [field: SerializeField] public AudioClip DriftStart { get; private set; }
    [field: SerializeField] public AudioClip DriftProgress { get; private set; }

    public void SetAudioClips(AudioClip move, AudioClip strike, AudioClip jumpStart, AudioClip jumpEnd) {
        Move = move;
        Strike = strike;
        DriftStart = jumpStart;
        DriftProgress = jumpEnd;
    }
}
