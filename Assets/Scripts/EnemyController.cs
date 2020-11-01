using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2d;
    public float timer;
    int direction = 1;

    Animator animator;

    // Start is called before the first frame update
    void Start() {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;

        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }

        if (vertical) {
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        } else {
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

    }

    private void FixedUpdate() {
        Vector2 position = rigidbody2d.position;
        
        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
        } else {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) {
        RubyController controller = other.gameObject.GetComponent<RubyController>();

        if (controller != null) {
            controller.ChangeHealth(-1);
        }
    }

}
