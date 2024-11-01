public class NoMoveState : ICharacterState
{
    private Character _character;
    private CharacterStateSwitcher _characterStateSwitcher;
    private CharacterView _characterView;

    private bool _isRunningState;

    public NoMoveState(Character character, CharacterStateSwitcher characterStateSwitcher)
    {
        _character = character;
        _characterView = _character.View;
        _characterStateSwitcher = characterStateSwitcher;
    }

    public void EnterState()
    {
        _isRunningState = true;

        if (_character.MeshAgent != null)
            _character.MeshAgent.isStopped = true;

        _characterView.StartIdling();
    }

    public void ExitState()
    {
        _isRunningState = false;

        if (_character.MeshAgent != null)
            _character.MeshAgent.isStopped = false;

        _characterView.StopIdling();
    }

    public void UpdadeState()
    {
        if (_isRunningState == false)
            return;

        if (_characterStateSwitcher.IsArrivedToTargetClick() == false && _character.IsDetonate == false)
        {
            if (_characterStateSwitcher.GetPath())
                _characterStateSwitcher.SetCharacterState(_characterStateSwitcher.MoveToTargetState);

        }

        //Debug.Log($"Крутим состояние NoMoveState!");
    }

}
