using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleAttack : MonoBehaviour
{
    public float speed;
    public float direction;
    public Animator anim;
    public float attackPoint;

    private Rigidbody2D rigidbody2d;

    // Start is called before the first frame update

    private void Awake()
    {
        attackPoint = GameSaver.difficultySetting[GameSaver.difficulty]["puzzleAttack"];
    }

    void Start()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        transform.localScale = new Vector2(direction * transform.localScale.x, transform.localScale.y);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = new Vector2(speed * -direction, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("WorldBoundary"))
        {
            anim.Play("Hit");
        }
    }

    public void destroy()
    {
        Destroy(gameObject);
    }

    

}
