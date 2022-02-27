using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FitnessModel
{
    public class AnimationState : MonoBehaviour
    {
        Animator _Animator;
        public bool _AnimationFinished = true;
        public enum CharacterStates
        {
            none,
            Idle,
            Jump,
            Throw,
            Squat,
            Point
        }

        public CharacterStates characterStates = CharacterStates.none;

        private void Awake()
        {
            _Animator = GetComponent<Animator>();
        }
        
        public void FinishAnimationState()
        {

            _AnimationFinished = true;
        }

        public void StartAnimationState()
        {

            _AnimationFinished = false;
        }

        public string CharacterState
        {
            get { return characterStates.ToString(); }
        }

        public void NextState(string state)
        {
            string methodName = state.ToString() + "State";
            System.Reflection.MethodInfo info =
                GetType().GetMethod(methodName,
                                    System.Reflection.BindingFlags.NonPublic |
                                    System.Reflection.BindingFlags.Instance);
            StartCoroutine((IEnumerator)info.Invoke(this, null));
        }

        IEnumerator IdleState()
        {
            characterStates = CharacterStates.Idle;
            yield return 0;
        }

        IEnumerator JumpState()
        {
            
            characterStates = CharacterStates.Jump;
            PlayAnimation();
            while (!_AnimationFinished)
            {
                yield return 0;
            }
            NextState("Idle");
        }
        IEnumerator ThrowState()
        {
            characterStates = CharacterStates.Throw;
            PlayAnimation();
            while (!_AnimationFinished)
            {
                yield return 0;
            }
            NextState("Idle");
        }

        IEnumerator SquatState()
        {
            characterStates = CharacterStates.Squat;
            PlayAnimation();         
            while (!_AnimationFinished)
            {
                yield return 0;
            }
            NextState("Idle");
        }

        IEnumerator PointState()
        {
            characterStates = CharacterStates.Point;
            PlayAnimation();
            while (!_AnimationFinished)
            {
                yield return 0;
            }
            NextState("Idle");
        }

        public void PlayAnimation()
        {
            switch (characterStates.ToString())
            {
                case "Throw":
                    _Animator.SetTrigger("Throw");
                    break;
                case "Jump":
                    _Animator.SetTrigger("Jump");
                    break;
                case "Squat":
                    _Animator.SetTrigger("Squat");
                    break;
                case "Point":
                    _Animator.SetTrigger("Point");
                    break;
                default:
                    Debug.Log("No Animation + " + characterStates.ToString());
                    break;
            }
        }

    }

}
