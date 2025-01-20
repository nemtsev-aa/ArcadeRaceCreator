using System;
using UnityEngine;

[System.Serializable]
public class TrackSegmentConfig {
    [field: SerializeField] public TrackSegmentType Type { get; private set; }
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public GameObject Prefab { get; private set; }
}

