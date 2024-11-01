using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private const string TakeDamageTriggerKey = "TakeDamage";
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int IsIdlingKey = Animator.StringToHash("IsIdling");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");
    private int _woundedLayerIndex;

    private Animator _animator;

    public void Initialise()
    {
        _animator = GetComponentInParent<Animator>();
        _woundedLayerIndex = _animator.GetLayerIndex("WoundedMask");
    }

    public void StartIdling()
    {
        _animator.SetBool(IsIdlingKey, true);
    }

    public void StopIdling()
    {
        _animator.SetBool(IsIdlingKey, false);
    }

    public void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);
    }

    public void StopRunning()
    {
        _animator?.SetBool(IsRunningKey, false);
    }

    public void StartWoundedWalk()
    {
        _animator.SetLayerWeight(_woundedLayerIndex, 1);
    }

    public void StopWoundedWalk()
    {
        _animator.SetLayerWeight(_woundedLayerIndex, 0);
    }

    public void AnimationTakeDamage()
    {
        _animator.SetTrigger(TakeDamageTriggerKey);
    }

    public void StartAnimationDead()
    {
        _animator.SetBool(IsDeadKey, true);
    }

    public void StopAnimationDead()
    {
        _animator.SetBool(IsDeadKey, false);
    }
}
