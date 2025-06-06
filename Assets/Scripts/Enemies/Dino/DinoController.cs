using UnityEngine;

public class DinoController : MonoBehaviour, IDamageDealer
{
    [Header("Dino Stats")]
    [SerializeField] private EnemiesRuntimeStats _stats;
    [SerializeField] private EnemiesStatsSO _baseStats;

    // Movement------------------
    private DinoMovement _dinoMovement;

    // References ---------------
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        //initialize the dino stats
        _stats = new EnemiesRuntimeStats(_baseStats);

        //initialize the references
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        //initialize the child scripts
        _dinoMovement = GetComponent<DinoMovement>();

        //set the move speed of the dino
        _dinoMovement.MoveSpeed = _stats.MoveSpeed;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
            _dinoMovement.StunTime = 1f;
    }

    //function to receive damage
    public void TakeDamage(float _damage)
    {
        _stats.CurrentHP -= _damage;
        if (_stats.CurrentHP <= 0f)
            Destroy(this.gameObject);
    }

    public int GetDamage()
    {
        return _stats.BaseDamage;
    }
}
