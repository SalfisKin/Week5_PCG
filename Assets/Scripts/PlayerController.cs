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

    private GameObject hitbox;

    float dashTime = float.PositiveInfinity;
    Rigidbody2D rb;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hitbox = transform.Find("HitBox").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
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
}
