using UnityEngine;

namespace Player
{
    public class PlayerTakeDamage : MonoBehaviour, IPlayerTakeDamage
    {
        private void OnDisable()
        {
            if (OnDamageEvent == null) return;
            foreach (var d in OnDamageEvent.GetInvocationList()) OnDamageEvent -= (DamageEventHandler)d;
        }

        public void PlayerTakeHit()
        {
            Debug.Log("PlayerTakeHit");
            OnDamageEvent?.Invoke();
        }

        public event DamageEventHandler OnDamageEvent;

        public void Subscribe(DamageEventHandler handler)
        {
            OnDamageEvent -= handler;
            OnDamageEvent += handler;
        }

        public void UnSubscribe(DamageEventHandler handler)
        {
            OnDamageEvent -= handler;
        }
    }
}