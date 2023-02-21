using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{
    public float speed;
    public float startWaitTime;
    public float radius;//xinjia

    private float waitTime;
    private Transform playerTransform;
    private bool attacking = false;

    public Transform movePos; //下一次要移动的位置坐标
    public Transform leftDownPos;
    public Transform rightUpPos;//飞行范围


    // Start is called before the first frame update
   public void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();//xinjia
        waitTime = startWaitTime;
        movePos.position = GetRandomPos();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();//调用父类的Update()方法
        if (playerTransform != null)//xinjia
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;//计算两点之间的距离
            if (distance < radius)
            {
                attacking = true;
            }
            else { attacking = false; }
        }

        if (!attacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);//移动到生成的随机坐标
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, (speed+0.5f) * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)//判断是否到新的坐标
        {
            if (waitTime <= 0)//蝙蝠在那停留的时间
            {
                movePos.position = GetRandomPos();//如果到了就继续生成一个新的坐标
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2  GetRandomPos()//获得随机的位置
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
