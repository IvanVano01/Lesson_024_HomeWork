using UnityEngine;

public class Detonator
{
    private ExplosionViewFX _explosionViewFX;

    private float _strengthExplosion;
    private float _radiusExplosion;
    private int _damageExplosion;
    private Vector3 _explosionCenterPosition;

    public Detonator(float strengthExplosion, int damageExplosion, Vector3 explosionCenterPosition, float radiusExplosion, ExplosionViewFX explosionViewFX)
    {
        _strengthExplosion = strengthExplosion;
        _damageExplosion = damageExplosion;
        _explosionCenterPosition = explosionCenterPosition;
        _radiusExplosion = radiusExplosion;
        _explosionViewFX = explosionViewFX;
    }

    public void CastExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(_explosionCenterPosition, _radiusExplosion);

        foreach (Collider colider in colliders)
        {
            if (colider.TryGetComponent<IDetonatable>(out IDetonatable detonatable))
            {
                Vector3 explosionDirection = (detonatable.transform.position - _explosionCenterPosition) + Vector3.up;

                detonatable.OnDetonate(explosionDirection, _strengthExplosion, _damageExplosion);
            }

            _explosionViewFX.OnPlayVFX(_explosionCenterPosition);
        }
    }
}
