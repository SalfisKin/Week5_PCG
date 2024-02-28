using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SearchService;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private Animator animator;

    public float attackCooldown;

    private float attackCooldownTimer;

    private GameObject hitbox;

    float dashTime = float.PositiveInfinity;
    Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        attackCooldownTimer = 0;rb = GetComponent<Rigidbody2D>();
        hitbox = transform.Find("HitBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldownTimer -= Time.deltaTime;

        if(Input.GetButton("Fire1") && attackCooldownTimer <0)
        {
            Debug.Log("Player attack!");
            animator.SetTrigger("Attack");
            attackCooldownTimer = attackCooldown;

            StartCoroutine(Attack());
        }

        dashTime += Time.deltaTime;
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");


        // if (input.x != 0) input.y = 0; //Make sure that the player only moves in four direction, not the diagonal.

        transform.Rotate(new Vector3(0, 0, input.x * Time.deltaTime * 100));
        rb.position += (input.y > 0 ? input.y : 0) * (Vector2)transform.up * Time.deltaTime * 10;
        Debug.Log(rb.rotation);

        // animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y > 0 ? input.y : 0);
        if (Input.GetButton("Jump") && dashTime >= 1f)
        {
            dashTime = 0f;
        }

        if (Input.GetButton("Dash"))
        {

        }

        if (dashTime <= 0.1f)
        {
            rb.position += (Vector2)transform.up * Time.deltaTime * 20;
            hitbox.SetActive(false);
        } else
        {
            hitbox.SetActive(true);
        }

        // StartCoroutine(Move(rb.position));

        // animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;

    }


    public Vector3 AttackOffset;
    public float AttackRange;
    public LayerMask attackMask;

    public int damage;

    IEnumerator Attack()
    {
        Debug.Log("Player Tried Attack");
        yield return new WaitForSeconds(0.2f);
        
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, AttackRange, attackMask);
        if(colInfo != null)
        {
            var healthComp = colInfo.GetComponent<MonsterHealth>();
            if(healthComp != null)
                healthComp.takeDamage(damage);
        }
    }
    
}
