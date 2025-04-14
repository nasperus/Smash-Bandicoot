using UnityEngine;

namespace Player
{
    public class PlayerTakeDamage : MonoBehaviour, IPlayerTakeDamage
    {
        public event DamageEventHandler OnDamageEvent;

        private void OnEnable()
        {
            OnDamageEvent += PlayerTakeHit;
        }

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

      
        
    }
}