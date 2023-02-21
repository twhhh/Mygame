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

    public Transform movePos; //��һ��Ҫ�ƶ���λ������
    public Transform leftDownPos;
    public Transform rightUpPos;//���з�Χ


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
        base.Update();//���ø����Update()����
        if (playerTransform != null)//xinjia
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;//��������֮��ľ���
            if (distance < radius)
            {
                attacking = true;
            }
            else { attacking = false; }
        }

        if (!attacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePos.position, speed * Time.deltaTime);//�ƶ������ɵ��������
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, (speed+0.5f) * Time.deltaTime);
        }

        if (Vector2.Distance(transform.position, movePos.position) < 0.1f)//�ж��Ƿ��µ�����
        {
            if (waitTime <= 0)//��������ͣ����ʱ��
            {
                movePos.position = GetRandomPos();//������˾ͼ�������һ���µ�����
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    Vector2  GetRandomPos()//��������λ��
    {
        Vector2 rndPos = new Vector2(Random.Range(leftDownPos.position.x, rightUpPos.position.x), Random.Range(leftDownPos.position.y, rightUpPos.position.y));
        return rndPos;
    }
}
