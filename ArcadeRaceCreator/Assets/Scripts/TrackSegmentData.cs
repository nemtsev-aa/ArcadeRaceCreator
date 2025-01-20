using System;
using UnityEngine;

[Serializable]
public class TrackSegmentData {
    public string SegmentType;
    public Vector3 Position;
    public Quaternion Rotation;

    public TrackSegmentData() {

    }

    public TrackSegmentData(string segmentType, Vector3 position, Quaternion rotation) {
        SegmentType = segmentType;
        Position = position;
        Rotation = rotation;
    }
}
