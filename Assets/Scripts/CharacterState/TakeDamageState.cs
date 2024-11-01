using UnityEngine;

public class TakeDamageState : ICharacterState
{
    private Character _character;
    private CharacterStateSwitcher _characterStateSwitcher;
    private CharacterView _characterView;

    private float _timerValueMax = 0.25f;
    private float _currentTimerValue;

    private bool _isRunningState;

    public TakeDamageState(Character character, CharacterStateSwitcher characterStateSwitcher)
    {
        _character = character;
        _characterView = _character.View;
        _characterStateSwitcher = characterStateSwitcher;
    }

    public void EnterState()
    {
        _isRunningState = true;

        _characterView.AnimationTakeDamage();

        _currentTimerValue = _timerValueMax;
    }

    public void ExitState()
    {
        _isRunningState = false;
    }

    public void UpdadeState()
    {
        if (_isRunningState == false)
            return;

        _currentTimerValue -= Time.deltaTime;

        if (_currentTimerValue < 0)
        {

            if (_characterStateSwitcher.GetPath() == false || _characterStateSwitcher.IsArrivedToTargetClick())
                _characterStateSwitcher.SetCharacterState(_characterStateSwitcher.NoMoveState);
            else
                _characterStateSwitcher.SetCharacterState(_characterStateSwitcher.MoveToTargetState);
        }

        //Debug.Log($"Крутим состояние TakeDamageState!");
    }
}
