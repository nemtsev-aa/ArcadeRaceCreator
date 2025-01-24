using System.IO;
using UnityEngine;
using Zenject;

public class LineFactory {
    private const string LinePrefab = "LinePrefab";
    private const string LineParentPrefab = "LineParentPrefab";
    private const string Prefabs = "Prefabs";

    private DiContainer _container;
    private Transform _parent;
    private Line _linePrefab;

    public LineFactory(DiContainer container) {
        _container = container;
      
        Load();
    }

    public Line Get() {
        return _container.InstantiatePrefabForComponent<Line>(_linePrefab, _parent);
    }

    private void Load() {
        _linePrefab = Resources.Load<Line>(Path.Combine(Prefabs, LinePrefab));
        _parent = Resources.Load<Transform>(Path.Combine(Prefabs, LineParentPrefab));
    }
}

