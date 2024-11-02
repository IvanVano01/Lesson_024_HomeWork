using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamageable, IDetonatable
{
    [Header("Configs")]
    [SerializeField] private float _maxSpeed;
    [SerializeField] private int _maxHealth;

    private int _percentWoundedForSlowing = 30;
    private int _currentHealth;

    private float _timeExplosionMax = 0.15f;
    private float _currentTimeExplosion;

    private CharacterView _view;

    private NavMeshAgent _navMeshAgent;
    private InputHandler _inputHandler;
    private ScreenClickHandler _screenClickHandler;
    private CharacterStateSwitcher _characterStateSwitcher;

    private Vector3 _blastWaveDirection = Vector3.zero;
    private float _detonateStrenght;

    [field: SerializeField] public bool _isAlive { get; private set; }
    [field: SerializeField] public bool IsDetonate { get; private set; }

    public void Initialize(InputHandler inputHandler, ScreenClickHandler screenClickHandler)
    {
        _inputHandler = inputHandler;
        _screenClickHandler = screenClickHandler;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _maxSpeed;
        _view = GetComponentInChildren<CharacterView>();
        _view.Initialise();

        _currentHealth = _maxHealth;
        _isAlive = true;
        _view.StopWoundedWalk();
        _characterStateSwitcher = new CharacterStateSwitcher(_inputHandler, _screenClickHandler, this);
    }   

    private void Update()
    {
        if (_isAlive == false)
            return;

        _characterStateSwitcher.Update();

        if (IsDetonate)
        {
            MoveByBlastWave();
        }
    }

    public Transform Transform => transform;
    public CharacterView View => _view;
    public CharacterStateSwitcher CharacterStateSwitcher => _characterStateSwitcher;
    public NavMeshAgent MeshAgent => _navMeshAgent;

    public void TakeDamage(int damage)
    {
        if (_currentHealth < 0)
            Debug.LogError($" Внимание! попытка нанести урон, когда уровень здоровья равен {_currentHealth}");

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;

            ToDie();

            return;
        }

        if (_currentHealth < (_maxHealth * _percentWoundedForSlowing) / 100)
        {
            float SlowSpeed = _maxSpeed / 3f;

            _navMeshAgent.speed = SlowSpeed;
            _view.StartWoundedWalk();
        }

        _characterStateSwitcher.SetCharacterState(_characterStateSwitcher._takeDamageState);
        Debug.Log($" У игрока осталось здоровья {_currentHealth}");
    }

    public void OnDetonate(Vector3 detonateDirection, float detonateStrenght, int damage)
    {
        TakeDamage(damage);

        IsDetonate = true;
        _currentTimeExplosion = _timeExplosionMax;
        _blastWaveDirection = detonateDirection;
        _detonateStrenght = detonateStrenght;
    }

    private void MoveByBlastWave()
    {
        _currentTimeExplosion -= Time.deltaTime;

        Vector3 direction = _blastWaveDirection - transform.position;

        transform.Translate(direction * _detonateStrenght / 2 * Time.deltaTime, Space.World);

        if (_currentTimeExplosion < 0)
        {
            IsDetonate = false;
        }
    }

    private void ToDie()
    {
        GetComponentInChildren<Collider>().enabled = false;

        _view.StopWoundedWalk();

        _characterStateSwitcher.SetCharacterState(_characterStateSwitcher._dyingState);

        _isAlive = false;

        Debug.Log($" Игрок мертв! Уровень здоровья = {_currentHealth}");
    }


}
