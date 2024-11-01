public class DyingState : ICharacterState
{
    private Character _character;
    private CharacterView _characterView;

    private bool _isRunningState;

    public DyingState(Character character)
    {
        _character = character;
        _characterView = _character.View;
    }

    public void EnterState()
    {
        _isRunningState = true;

        _character.MeshAgent.isStopped = true;
        _characterView.StartAnimationDead();        
    }

    public void ExitState()
    {
        _isRunningState = false;
        _characterView.StopAnimationDead();

        if (_character.MeshAgent != null)
            _character.MeshAgent.isStopped = false;
    }

    public void UpdadeState()
    {
        if (_isRunningState == false)
            return;

        //Debug.Log($"Крутим состояние DyingState!");
    }
}
