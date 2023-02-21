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
            
            anim.SetTrigger("Attack");//���Ź�������
            StartCoroutine(StartAttack());

        }
    }
    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(startTime);
        collider2D.enabled = true;//�ܵ��������ϰ���ײ����Ϊtrue
        StartCoroutine(disableHixBox());
    }
    IEnumerator disableHixBox()//������֮���ù�����ײ����ʧ
    {
        yield return new WaitForSeconds(time);
        collider2D.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))//�Ƿ���ײ����enemy
        {
            other.GetComponent<Enemy>().TakeDamage(damage);

        }

        if (other.gameObject.CompareTag("YellowStar"))//�Ƿ���ײ����enemy
        {
            other.GetComponent<YellowStar>().GenGift();

        }
    }
}
