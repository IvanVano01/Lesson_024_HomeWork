using UnityEngine;

public class Rotator
{
    private Transform _transform;
    private float _speedRotation;
    private float _deadZone = 0.01f;

    public Rotator(Transform transform, float speedRotation)
    {
        _transform = transform;
        _speedRotation = speedRotation;
    }

    public void RotateTo(Vector3 direction)
    {
        if (direction.magnitude > _deadZone)
            return;

        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);

        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, _speedRotation * Time.deltaTime);
        _transform.rotation = lookRotation;
    }
}
