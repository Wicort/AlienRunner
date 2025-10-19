using System.Collections;
using UnityEngine;

namespace Assets._Project._scripts.Weapon
{
    public class Gun : MonoBehaviour, IShootable
    {
        [SerializeField] private ParticleSystem _bulletEjection;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private GameObject _shootPoint;

        public void Shoot()
        {
            StartCoroutine(PlayParticleEffect());
            Instantiate(_bulletPrefab, _shootPoint.transform);
        }

        private IEnumerator PlayParticleEffect()
        {
            _bulletEjection.Play();
            yield return new WaitForSeconds(.2f);
            _bulletEjection.Stop();
        }
    }
}
