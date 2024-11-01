using UnityEngine;

public interface IDetonatable
{
    Transform transform { get; }

    void OnDetonate(Vector3 detonateDirection, float detonateStrenght, int detonateDamage);
}
