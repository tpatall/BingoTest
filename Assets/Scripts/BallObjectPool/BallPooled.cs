using UnityEngine;

/// <summary>
///     This class has the functionality of a ball GameObject for after it has been spawned in.
/// </summary>
public class BallPooled : MonoBehaviour, IBallPooled
{
    /// <summary>
    ///     The time passed since creation.
    /// </summary>
    private float lifeTime;

    /// <summary>
    ///     The maximum allowed time alive.
    /// </summary>
    private float maxLifeTime;

    /// <summary>
    ///     The pool that manages these objects.
    /// </summary>
    private BallObjectPooler pool;

    /// <summary>
    ///     Upon instantiation, if a pool has not been created, create a new pool.
    /// </summary>
    public BallObjectPooler Pool
    {
        get { return pool; }
        set
        {
            if (pool == null)
                pool = value;
            else
                throw new System.Exception("This should only get set once.");
        }
    }

    /// <summary>
    ///     Reset the lifetime when this GameObject has been enabled.
    /// </summary>
    private void OnEnable()
    {
        lifeTime = 0f;
        maxLifeTime = GetComponent<ParticleSystem>().main.startLifetime.constant;
    }

    /// <summary>
    ///     Update the position based on the object count. If this object is the first in the pool, then remove it.
    /// </summary>
    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > maxLifeTime)
        {
            pool.ReturnToPool(this.gameObject);
        }
    }
}