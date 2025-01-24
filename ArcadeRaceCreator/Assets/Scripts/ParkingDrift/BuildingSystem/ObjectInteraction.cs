using UnityEngine;

public class ObjectInteraction : MonoBehaviour {
    private MeshCollider _meshCollider;
    private Rigidbody _rigidbody;

    private void Start() {
        _meshCollider ??= GetComponent<MeshCollider>();
        _rigidbody ??= GetComponent<Rigidbody>();

        Select(false);
    }

    public void Select(bool status) {
        _meshCollider.enabled = !status;
        //_rigidbody.isKinematic = !status;
        //_rigidbody.useGravity = status;
    }

    public ObjectInteractionData GetData() {
        return new ObjectInteractionData(
            gameObject.name,
            transform.position,
            transform.localEulerAngles,
            transform.localScale
            );
    }
}
