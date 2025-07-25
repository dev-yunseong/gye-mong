using System.Collections;
using UnityEngine;

namespace GyeMong.EventSystem.Event.CinematicEvent
{
    public abstract class AnimationControllEvent : CinematicEvent
    {
        [SerializeField] protected Animator _animator;
    }

    public class StopAnimatorEvent : AnimationControllEvent
    {
        public override IEnumerator Execute(EventObject eventObject = null)
        {
            _animator.enabled = false;
            return null;
        }
    }

    public class StartAnimatorEvent : AnimationControllEvent
    {
        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }
        public override IEnumerator Execute(EventObject eventObject = null)
        {
            _animator.enabled = true;
            return null;
        }
    }

    public class SetAnimatorParameter : AnimationControllEvent
    {
        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }
        public void SetParameter(string name, float value)
        {
            _name = name;
            _value = value;
        }
        public enum CreatureType
        {
            NonPlayableCharacter,
            Player,
        }
        [SerializeField] public CreatureType _creatureType;
        [SerializeField]
        private string _name;
        [SerializeField]
        private float _value;
        public override IEnumerator Execute(EventObject eventObject = null)
        {
            if (_creatureType == CreatureType.NonPlayableCharacter)
                _animator.SetFloat(_name, _value);
            else SceneContext.Character.GetComponent<Animator>().SetFloat(_name, _value);
            return null;
        }
    }
    public class SetAnimatorParameterBool : AnimationControllEvent
    {
        public void SetAnimator(Animator animator)
        {
            _animator = animator;
        }

        public void SetParameter(string name, bool value)
        {
            _name = name;
            _value = value;
        }
        public enum CreatureType
        {
            NonPlayableCharacter,
            Player,
        }

        [SerializeField] private CreatureType _creatureType;
        [SerializeField] private string _name;
        [SerializeField] private bool _value;

        public override IEnumerator Execute(EventObject eventObject = null)
        {
            if (_creatureType == CreatureType.NonPlayableCharacter)
                _animator.SetBool(_name, _value);
            else
                SceneContext.Character.GetComponent<Animator>().SetBool(_name, _value);

            return null;
        }
    }
}