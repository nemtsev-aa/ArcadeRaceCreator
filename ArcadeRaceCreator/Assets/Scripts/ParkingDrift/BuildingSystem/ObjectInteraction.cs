using UnityEngine;

public class ObjectInteraction : MonoBehaviour {
    private MeshCollider _meshCollider;
    private Rigidbody _rigidbody;

    public void Init() {
        _meshCollider ??= GetComponent<MeshCollider>();
        _rigidbody ??= GetComponent<Rigidbody>();

        Select(false);
        SetPhysic(false);
    }

    public void SetPhysic(bool status) {
        _rigidbody.useGravity = status;
        _rigidbody.isKinematic = !status;
    }

    public void Select(bool status) {
        _meshCollider.enabled = !status;
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
