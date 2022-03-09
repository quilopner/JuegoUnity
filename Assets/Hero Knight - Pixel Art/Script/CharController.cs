using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public float velocidad = 5;
    public float fuerza;
    public float maxsaltos = 30;
    public LayerMask suelo;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool derecha = true;
    private float saltosrest;
    private Animator animator;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosrest = maxsaltos;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Movimiento();
        Salto();
    }

    bool TocaSuelo()
    {
        RaycastHit2D raycastHit =
    Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down,0.2f,suelo);
        return raycastHit.collider != null;    
    }

    void Salto()
    {
        if(TocaSuelo())
        {
            saltosrest = maxsaltos;
        }

        if(Input.GetKeyDown(KeyCode.Space) && saltosrest > 0)
        {
            saltosrest--;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0f);
            rigidBody.AddForce(Vector2.up * fuerza, ForceMode2D.Impulse);
        }
    }

    void Movimiento()
    {
        float inputMovimiento = Input.GetAxis("Horizontal");


        if(inputMovimiento != 0f)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    void GestionarOrientacion(float inputMovimiento)
    {
        if(derecha == true && inputMovimiento < 0 || (derecha == false && inputMovimiento > 0))
        {
            derecha = !derecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}

