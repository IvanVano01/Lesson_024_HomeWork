using UnityEngine;

public class ExplosionViewFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleExplosionVFXPrefab;

    public void OnPlayVFX(Vector3 position)
    {
        Instantiate(_particleExplosionVFXPrefab, position, Quaternion.identity, null);
    }
}
