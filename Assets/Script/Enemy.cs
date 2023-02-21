using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public int damage;
    public float flashTime;
    public GameObject bloodEffect;
    public GameObject dropCoin;
    public GameObject floatPoint;

    private SpriteRenderer sr;
    private Color originalColor;
    private PlayerHealth playerHealth;
    // Start is called before the first frame update
    public void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();//��ȡPlayer�µ�PlayerHealth�ű�������������Ķ���
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin,transform.position,Quaternion.identity);//�������󣬲�����λ�úͽǶ�
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)//��player����
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject ;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= damage;
        FlashColor(flashTime);//����Ч��
        Instantiate(bloodEffect,transform.position,Quaternion.identity);//������Ѫ��Ч 
       GameController.camShake.Shake();

    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//���ֺ�ɫ������
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       // UnityEngine.CapsuleCollider2D
        //��ײ������Player������ײ����һ����������ײ��
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }

        }
    }

}
