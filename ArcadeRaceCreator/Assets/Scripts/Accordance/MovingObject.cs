using UnityEngine;

public class MovingObject : MonoBehaviour {
    private Line _line;
    private float _speed = 10f;

    private Vector3[] _positions;
    private int _moveIndex = 0;
    private bool _startMovement = false;

    public void Init(Line line, float speed) {
        _line = line;
        _speed = speed;

        _positions = new Vector3[_line.Renderer.positionCount];
        _line.Renderer.GetPositions(_positions);

        _startMovement = true;
    }

    private void Update() {
        if (_startMovement == true) {
            Vector2 currentPosition = _positions[_moveIndex];
            transform.position = Vector2.MoveTowards(transform.position, currentPosition, _speed * Time.deltaTime);

            float distance = Vector2.Distance(currentPosition, transform.position);

            if (distance <= 0.05f)
                _moveIndex++;

            if (_moveIndex > _positions.Length - 1)
                _startMovement = false;
        }
    }
}
