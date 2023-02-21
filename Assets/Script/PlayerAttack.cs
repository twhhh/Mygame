using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int damage;
    public float time;
    public float startTime;

    private Animator anim;
    private PolygonCollider2D collider2D;
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        collider2D = GetComponent<PolygonCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGameAlive)
        {
            Attack();
        }
    }

    void Attack() 
    {
        if (Input.GetButtonDown("Attack"))
        {
            
            anim.SetTrigger("Attack");//播放攻击动画
            StartCoroutine(StartAttack());

        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;//受到攻击马上把碰撞框设为true
        StartCoroutine(disableHixBox());
    }
    IEnumerator disableHixBox()//攻击完之后让攻击碰撞框消失
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))//是否碰撞到了enemy
        {
            other.GetComponent<Enemy>().TakeDamage(damage);

        }

        if (other.gameObject.CompareTag("YellowStar"))//是否碰撞到了enemy
        {
            other.GetComponent<YellowStar>().GenGift();

        }
    }
}
