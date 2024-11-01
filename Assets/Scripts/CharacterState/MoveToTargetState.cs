using UnityEngine;
using UnityEngine.AI;

public class MoveToTargetState : ICharacterState
{
    private Character _character;
    private CharacterStateSwitcher _characterStateSwitcher;
    private ScreenClickHandler _screenClickHandler;
    private CharacterView _characterView;
    private NavMeshAgent _navMeshAgent;

    private bool _isRunningState;

    public MoveToTargetState(Character character, ScreenClickHandler screenClickHandler, CharacterStateSwitcher characterStateSwitcher)
    {
        _character = character;
        _screenClickHandler = screenClickHandler;
        _characterStateSwitcher = characterStateSwitcher;

        _navMeshAgent = _character.MeshAgent;
        _characterView = _character.View;
    }

    public void EnterState()
    {
        _isRunningState = true;

        _characterView.StartRunning();
    }

    public void ExitState()
    {
        _isRunningState = false;

        _characterView.StopRunning();
    }

    public void UpdadeState()
    {

        if (_character.IsDetonate == false)
        {
            if (_characterStateSwitcher.IsArrivedToTargetClick() || _characterStateSwitcher.GetPath() == false)
                _characterStateSwitcher.SetCharacterState(_characterStateSwitcher.NoMoveState);
        }

        if (_isRunningState)
            MoveTo(_screenClickHandler.TargetAgentPosition);

        //Debug.Log($"Крутим состояние MoveToTargetState!");
    }

    public void SetSpeed(float speed) => _navMeshAgent.speed = speed;

    private void MoveTo(Vector3 targetAgentPosition)
    {
        if (_character.MeshAgent != null)
            _navMeshAgent.isStopped = false;

        _navMeshAgent.SetDestination(targetAgentPosition);
    }
}
