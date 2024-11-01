using UnityEngine;

public class Crate : MonoBehaviour, IDetonatable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnDetonate(Vector3 detonateDirection, float detonateStrenght, int detonateDamage)
    {
        _rigidbody.AddForce(detonateDirection * detonateStrenght,ForceMode.Impulse);
    }
}
