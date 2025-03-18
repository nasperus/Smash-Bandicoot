using UnityEngine;

namespace Player
{
    public delegate void SpinDamageDelegate();
    public interface IPlayerDoesDamage
    {
        void DoSpinDamage();
    }
}
