using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets._Project._scripts.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;    
        [SerializeField] private float duration = 3f;
        private void Start()
        {
            Vector3 direction = transform.forward;
            Vector3 targetPosition = transform.position + direction * speed * duration;

            transform.DOMove(targetPosition, duration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    Destroy(gameObject); 
                });
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}
