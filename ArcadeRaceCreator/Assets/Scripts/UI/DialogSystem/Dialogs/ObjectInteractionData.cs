using System;
using UnityEngine;

[Serializable]
public class ObjectInteractionData {
    public ObjectInteractionData(string name, Vector3 position, Vector3 rotation, Vector3 scale) {
        Name = name;
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }

    public string Name { get; private set; }
    public Vector3 Position { get; private set; }
    public Vector3 Rotation { get; private set; }
    public Vector3 Scale { get; private set; }
}
