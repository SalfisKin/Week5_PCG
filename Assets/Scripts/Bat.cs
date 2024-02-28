using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public Transform player;

	public bool isFlipped = false;

	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

    Rigidbody2D rb;
    public Vector3 AttackOffset;
    public float AttackRange;
    public LayerMask attackMask;

    public int damage;

    public void Attack()
    {
        Debug.Log("Bat Tried Attack");
        Vector3 pos = transform.position;
        pos += transform.right * AttackOffset.x;
        pos += transform.up * AttackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, AttackRange, attackMask);
        if (colInfo != null)
        {
            //Damage the player!
            Debug.Log("Bat Attack hit on player!");
            var healthComp = colInfo.GetComponent<PlayerHealth>();
            if(healthComp != null)
                healthComp.takeDamage(damage);
        }
    }

    

    void Die()
    {
        Debug.Log("Bat died!");
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        var healthComp = GetComponent<MonsterHealth>();
        healthComp.onDeath += Die;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
