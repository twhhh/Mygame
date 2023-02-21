using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionRange;
    public Vector2 startSpeed;
    public float delayExplodeTime;
    public float destroyBombTime;
    public float hixBoxTime;

    private Rigidbody2D rb2d;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rb2d.velocity = transform.right * startSpeed.x + transform.up * startSpeed.y;

        Invoke("Explode", delayExplodeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode()
    {
        anim.SetTrigger("Explode");
        Invoke("GenExplosionRange", hixBoxTime);
        Invoke("DestroyThisBomb", destroyBombTime);

    }

    void GenExplosionRange()
    {
        Instantiate(explosionRange, transform.position, Quaternion.identity);//quaternin.iden是四元数（0,0,0,0）
    }

    void DestroyThisBomb()
    {
        Destroy(gameObject);
    }
}
