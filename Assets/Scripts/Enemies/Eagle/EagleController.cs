using UnityEngine;

public class EagleController : MonoBehaviour,IDamageDealer
{
    [Header("Eagle Stats")]
    [SerializeField] private EnemiesStatsSO _statsSO;
    [SerializeField] private EnemiesRuntimeStats _stats;

    [Header("References")]
    public Rigidbody2D Rigidbody2D { get; private set; }


    void Awake()
    {
        //initialize the stats
        _stats = new EnemiesRuntimeStats(_statsSO);

        //initialize the references
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public int GetDamage()
    {
        return _stats.BaseDamage;
    }

}
