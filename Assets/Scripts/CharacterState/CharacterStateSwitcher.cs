using UnityEngine;
using UnityEngine.AI;

public class CharacterStateSwitcher
{
    private InputHandler _inputHandler;
    private ScreenClickHandler _screenClickHandler;
    private Character _character;
    private NavMeshAgent _navMeshAgent;

    public ICharacterState _noMoveState;
    public ICharacterState _moveToTargetState;
    public ICharacterState _takeDamageState;
    public ICharacterState _dyingState;

    private ICharacterState _currentCharacterstate;
    private float _deadZone = 0.25f;

    private Vector3 _targetAgentPosition => _screenClickHandler.TargetAgentPosition;

    public CharacterStateSwitcher(InputHandler inputHandler, ScreenClickHandler screenClickHandler, Character character)
    {
        _inputHandler = inputHandler;
        _screenClickHandler = screenClickHandler;
        _character = character;
        _navMeshAgent = _character.MeshAgent;

        _noMoveState = new NoMoveState(_character, this);
        _moveToTargetState = new MoveToTargetState(_character, _screenClickHandler, this);
        _takeDamageState = new TakeDamageState(_character, this);
        _dyingState = new DyingState(_character);

        SetCharacterState(_noMoveState);
    }

    public ICharacterState CurrentCharacterstate => _currentCharacterstate;
    public ICharacterState NoMoveState => _noMoveState;
    public ICharacterState MoveToTargetState => _moveToTargetState;
    public ICharacterState TakeDamageState => _takeDamageState;
    public ICharacterState DyingState => _dyingState;

    public void Update()
    {
        _inputHandler.Update();
        _screenClickHandler.Update();

        _currentCharacterstate.UpdadeState();
    }

    public void SetCharacterState(ICharacterState characterstate)
    {
        if (_currentCharacterstate == characterstate)
            return;

        _currentCharacterstate?.ExitState();
        _currentCharacterstate = characterstate;
        _currentCharacterstate.EnterState();
    }

    public bool IsArrivedToTargetClick()
    {
        if ((_targetAgentPosition - _character.Transform.position).magnitude < _deadZone)
            return true;

        return false;
    }

    public bool GetPath()
    {
        NavMeshPath pathTotarget = new NavMeshPath();

        pathTotarget.ClearCorners();

        if (_navMeshAgent.CalculatePath(_screenClickHandler.TargetAgentPosition, pathTotarget) && pathTotarget.status != NavMeshPathStatus.PathInvalid)
            return true;

        Debug.Log("Путь невалидный!");
        return false;
    }
}
