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
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();//获取Player下的PlayerHealth脚本，可以用里面的东西
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }

    // Update is called once per frame
    public void Update()
    {
        if (health <= 0)
        {
            Instantiate(dropCoin,transform.position,Quaternion.identity);//创建对象，并设置位置和角度
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)//给player调用
    {
        GameObject gb = Instantiate(floatPoint, transform.position, Quaternion.identity) as GameObject ;
        gb.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        health -= damage;
        FlashColor(flashTime);//红闪效果
        Instantiate(bloodEffect,transform.position,Quaternion.identity);//生成流血特效 
       GameController.camShake.Shake();

    }

    void FlashColor(float time)
    {
        sr.color = Color.red;
        Invoke("ResetColor", time);//保持红色多少秒
    }

    void ResetColor()
    {
        sr.color = originalColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
       // UnityEngine.CapsuleCollider2D
        //碰撞碰到了Player并且碰撞框是一个胶囊体碰撞框
        if (other.gameObject.CompareTag("Player") && other.GetType().ToString() == "UnityEngine.CapsuleCollider2D")
        {
            if (playerHealth != null)
            {
                playerHealth.DamagePlayer(damage);
            }

        }
    }

}
