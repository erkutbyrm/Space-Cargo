using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour, IPooledObject
{
    [SerializeField] private GameObject _laserHitExplosionPrefab;
    [SerializeField] private Rigidbody2D _rigidBody;
    public virtual float BulletVelocity { get; set; } = 20f;
    public virtual int LaserDamage { get; set; } = 1;
    private bool _isReturned = false;
   

    public void OnSpawnFromPool()
    {
        _isReturned = false;
        _rigidBody.velocity = transform.right * BulletVelocity;
        AudioManager.Instance.PlaySoundWithName("LaserShootSound");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject hitExplosion = Instantiate(_laserHitExplosionPrefab, transform.position, transform.rotation);
        AudioManager.Instance.PlaySoundWithName("LaserHitSound");

        Animator animator = hitExplosion.GetComponent<Animator>();
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        Destroy(hitExplosion, delay);
        
        if (!_isReturned)
        {
            ReturnToQueue();
        }
    }

    private void OnBecameInvisible()
    {
        if(!_isReturned)
        {
            ReturnToQueue();
        }
    }

    private void ReturnToQueue()
    {
        _isReturned = true;
        gameObject.SetActive(false);
        ObjectPooler.Instance.ReturnBackToQueue(transform.tag, gameObject);
    }
}
