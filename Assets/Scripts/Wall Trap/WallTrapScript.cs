using DG.Tweening;
using UnityEngine;

namespace Wall_Trap
{
    public class WallTrapScript : MonoBehaviour
    {
        private enum MoveDirection { Left, Right }
        [SerializeField] private MoveDirection moveDirection; 
        [SerializeField] private float slightMoveDistance;
        [SerializeField] private float fastMoveDistance;
        [SerializeField] private float slightDuration;
        [SerializeField] private float fastDuration;
        [SerializeField] private float returnDuration;
        [SerializeField] private float shakeStrength;
        [SerializeField] private int shakeEffect;
        [SerializeField] private float loopDelay;
        
        private const int NegativeOne = -1;
        private const int PositiveOne = 1;
        private Vector3 _startPosition;
        private int _toggleDirection; 

        private void Start()
        {
            _startPosition = transform.position; 
            _toggleDirection = (moveDirection == MoveDirection.Left) ? NegativeOne : PositiveOne; 
            MoveWithEffect();
        }

        private void MoveWithEffect()
        {
            var moveSequence = DOTween.Sequence()
                .Append(MoveSlightLeftSlow())
                .Append(MoveBackToOriginalPositionSlow())
                .Append(ShakeEffect())
                .Append(MoveFastLeft())
                .Append(MoveSloBackToRight())
                .AppendInterval(loopDelay)
                                .SetLoops(-1);

            moveSequence.Play();
        }

        private Tween MoveSlightLeftSlow()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x + (_toggleDirection * slightMoveDistance), slightDuration)
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
                .Append(transform.DOShakePosition(0.3f, shakeStrength, shakeEffect));
        }

        private Tween MoveFastLeft()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x + (_toggleDirection * fastMoveDistance), fastDuration)
                    .SetEase(Ease.OutQuad));
        }

        private Tween MoveSloBackToRight()
        {
            return DOTween.Sequence()
                .Append(transform.DOMoveX(_startPosition.x, returnDuration)
                    .SetEase(Ease.InOutSine));
        }
        
    }
    }

