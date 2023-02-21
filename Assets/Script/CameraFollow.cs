using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;//角色所在坐标
    public float smoothing;//相机移动平滑因子

    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameController.camShake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();//获得CameraShake中的脚本,就可以在Enemy中调用

    }

    void LateUpdate()//晚于所有Update执行的
    {
        if (target != null)//意义就是如果角色死亡，用update就会报错,所以要if判别一下
        {
            if (transform.position != target.position) //如果相机位置和角色位置不一样
            {
                Vector3 targetPos = target.position;
                targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);//范围之间
                targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);//线性差值函数，如果smoothing是0.3，从a点到b点时间为0.3
            }

        }    
    }

    public void SetCamPosLimit(Vector2 minPos,Vector2 maxPos)
    {
        minPosition = minPos;
        maxPosition = maxPos;
    }
}
