using DG.Tweening;
using UnityEngine;

namespace Wall_Trap
{
    public class WallTrapScript : MonoBehaviour
    {
        [SerializeField] private MoveDirection moveDirection;
        [SerializeField] private float slightMoveDistance;
        [SerializeField] private float fastMoveDistance;
        [SerializeField] private float slightDuration;
        [SerializeField] private float fastDuration;
        [SerializeField] private float returnDuration;
        [SerializeField] private float shakeStrength;
        [SerializeField] private int shakeEffect;
        [SerializeField] private float shakeDuration;
        [SerializeField] private float loopDelay;
        private Vector3 _pushDirection;

        private Vector3 _startPosition;
        private int _toggleDirection;
        public bool IsPlayerDamageable { get; private set; }

        private void Start()
        {
            _startPosition = transform.position;
            _toggleDirection = moveDirection == MoveDirection.Left ? -1 : 1;
            MoveWithEffect();
        }

        private void Update()
        {
            transform.position = _startPosition;
        }


        private void MoveWithEffect()
        {
            var moveSequence = DOTween.Sequence()
                .Append(MoveSlightly())
                .Append(MoveBackToOriginalPositionSlow())
                .Append(ShakeEffect())
                .Append(MoveFast())
                .Append(MoveBackToOriginalPosition())
                .AppendInterval(loopDelay)
                .SetLoops(-1);
            moveSequence.Play();
        }


        private Tween MoveSlightly()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x + _toggleDirection * slightMoveDistance, slightDuration)
                    .SetEase(Ease.InOutSine));
        }

        private Tween MoveBackToOriginalPositionSlow()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x, slightDuration)
                    .SetEase(Ease.InOutSine));
        }

        private Tween ShakeEffect()
        {
            return DOTween.Sequence()
                .Append(transform.DOShakePosition(shakeDuration, shakeStrength, shakeEffect));
        }

        private Tween MoveFast()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x + _toggleDirection * fastMoveDistance, fastDuration)
                    .SetEase(Ease.OutQuad))
                .OnUpdate(() => { IsPlayerDamageable = true; });
        }

        private Tween MoveBackToOriginalPosition()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x, returnDuration)
                    .SetEase(Ease.InOutSine))
                .OnComplete(() => { IsPlayerDamageable = false; });
        }

        private enum MoveDirection
        {
            Left,
            Right
        }
    }
}