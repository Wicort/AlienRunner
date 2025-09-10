using Assets._Project._scripts.Player;
using System.Collections;
using UnityEngine;

namespace Assets._Project._scripts.Enemyes
{
    public class Orc : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private float speed;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerComponent player))
            {
                _animator.SetTrigger("Attack");
                StartCoroutine(Run());
            }
        }

        private IEnumerator Run()
        {
            while (true)
            {
                yield return null;
                transform.position -= new Vector3(0, 0, speed * Time.deltaTime);
            }
        }
    }
}
