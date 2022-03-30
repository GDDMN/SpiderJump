using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJump : MonoBehaviour
{
    private float jumpForce;
    //private bool jumping = true;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] GameObject web;
    [SerializeField] GameObject lrGO;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            if (rb.velocity.y <= 0) // проверяем, что мы не пролетаем через плафторму наверх
            {
                animator.SetBool("Jumping", false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryToHook();
        }
    }

    private void TryToHook()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 1, 0), Vector2.up, 2f);
        if (hit.collider.gameObject.CompareTag("Platform"))
        {
            var heading = hit.collider.gameObject.transform.position - (gameObject.transform.position + new Vector3(0, -0.5f, 0));
            var distance = heading.magnitude; // расстояние между паучком и таргет-платформой

            #region Line Renderer
            LineRenderer lr;
            lrGO.SetActive(true);
            lr = lrGO.GetComponent<LineRenderer>();
            Vector3 sp = transform.position;
            Vector3 ep = hit.collider.gameObject.transform.position;
            lr.SetVertexCount(2);
            lr.SetPosition(0, sp);
            lr.SetPosition(1, ep);
            #endregion

            float force = 5f;
            rb.Sleep();
            rb.WakeUp();
            Jump(force);
        }
    }

    private void Jump(float force)
    {
        //jumping = true;
        animator.SetBool("Jumping", true);
        rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }

}
