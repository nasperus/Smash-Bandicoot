using UnityEngine;

namespace Player
{
    public class PlayerAnimationScript : MonoBehaviour
    {
        private static readonly int Running = Animator.StringToHash("Running");
        private static readonly int Spin = Animator.StringToHash("Spin");
        private static readonly int Jump = Animator.StringToHash("Jump");

        [SerializeField] private Animator animator;

        private void Update()
        {
            PlayerAnimation();
        }

        private void PlayerAnimation()
        {
            animator.SetBool(Running, PlayerMovementScript.IsRunning);
        }

        public void SpinAnimation()
        {
            animator.SetTrigger(Spin);
        }

        public void JumpAnimation()
        {
            animator.SetTrigger(Jump);
        }
    }
}