using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float AttackCooldown;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject[] attacks;
    private Animator anim;
    private PlayerMovement playermovement;
    private float CooldownTimer = Mathf.Infinity;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && CooldownTimer > AttackCooldown)
            Attack();

        CooldownTimer = Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        CooldownTimer = 0;

        attacks[0].transform.position = attackPoint.position;
        attacks[0].GetComponent<Projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    private int WhichAttack()
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            if (!attacks[i].activeInHierarchy)
                return i;
        }


        return 0;

    }
}
