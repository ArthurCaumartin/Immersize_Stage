using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public Animator Animator { set => _animator = value; }

    public const string STATE_TAKE_DAMAGE = "TakeDamage";
    public const string STATE_ATTACK = "Attack";

    public void PlayState(string stateName)
    {
        // dans les animaiton on va add des events qui vont call des petit script
        // overlap attack / projectile spawner / etc...
        if (!_animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            _animator.Play(stateName);

        // AnimatorStateInfo info = _animator.GetCurrentAnimatorStateInfo(0);
        // stateLenth = info.length;
    }
}