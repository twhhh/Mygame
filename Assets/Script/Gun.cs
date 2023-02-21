using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{

    public GameObject bullet;
    public Transform muzzleTransform;
    public Camera cam;

    private Vector3 mousePos;//��¼���λ��
    private Vector2 gunDirection;//ǹ�ķ���
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);//��Ļ����ת��Ϊ��������
       
        gunDirection = (mousePos - transform.position).normalized;//��������������Ǹ�normalized�ǵ�λ��������˼
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;//��arctan�ٻ���ת�Ƕ�
        transform.eulerAngles = new Vector3(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fire!!!");
            Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        }
    }
}
