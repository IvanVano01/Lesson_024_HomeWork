using UnityEngine;

public class Mine : MonoBehaviour
{
    [Header("links")]
    [SerializeField] private ExplosionViewFX _explosionViewFXPrefab;

    [Header("Configs")]
    [SerializeField] private float _strengthExplosion;
    [SerializeField] private int _damageExplosion;
    [SerializeField] private float _radiusDetonate;
    [SerializeField] private float _timerDetonateDelay;

    [SerializeField] private float _currentTimer;
    private Detonator _detonator;
    private Collider _collider;

    private bool _isDetonate;

    private void Start()
    {
        this.gameObject.SetActive(true);
        _collider = GetComponent<Collider>();

        _currentTimer = _timerDetonateDelay;
    }

    private void Update()
    {
        if (_isDetonate == false)
            return;

        StartTimerToDetonate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusDetonate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDetonatable detonatable))
        {
            _collider.enabled = false;
            Vector3 explosionPosition = transform.position;

            _detonator = new Detonator(_strengthExplosion, _damageExplosion, explosionPosition, _radiusDetonate, _explosionViewFXPrefab);

            _currentTimer = _timerDetonateDelay;
            _isDetonate = true;
        }
    }

    private void StartTimerToDetonate()
    {
        _currentTimer -= Time.deltaTime;

        if (_currentTimer < 0)
        {
            _detonator.CastExplosion();

            _isDetonate = false;
            this.gameObject.SetActive(false);
        }
    }
}
