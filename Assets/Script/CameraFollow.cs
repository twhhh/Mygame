using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//��ɫ��������
    public float smoothing;//����ƶ�ƽ������

    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();//���CameraShake�еĽű�,�Ϳ�����Enemy�е���

    }

    void LateUpdate()//��������Updateִ�е�
    {
        if (target != null)//������������ɫ��������update�ͻᱨ��,����Ҫif�б�һ��
        {
            if (transform.position != target.position) //������λ�úͽ�ɫλ�ò�һ��
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);//��Χ֮��
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);//���Բ�ֵ���������smoothing��0.3����a�㵽b��ʱ��Ϊ0.3
            }

        }    
    }

    public void SetCamPosLimit(Vector2 minPos,Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
}
