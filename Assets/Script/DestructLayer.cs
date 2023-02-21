using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DestructLayer : MonoBehaviour
{
    public float offsetX;
    public float offsetY;


    private Tilemap destructLayer;

    private Vector3 pos1;
    private Vector3 pos2;
    private Vector3 pos3;
    private Vector3 pos4;
    private Vector3 pos5;
    private Vector3 pos6;
    private Vector3 pos7;
    private Vector3 pos8;
    // Start is called before the first frame update
    void Start()
    {
        destructLayer = GetComponent<Tilemap>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Vector3 hitPos = other.bounds.ClosestPoint(other.transform.position);
            pos1 = new Vector3(hitPos.x + offsetX, hitPos.y, 0f);
            pos2 = new Vector3(hitPos.x - offsetX, hitPos.y, 0f);
            pos3 = new Vector3(hitPos.x, hitPos.y + offsetY, 0f);
            pos4 = new Vector3(hitPos.x, hitPos.y - offsetY, 0f);
            pos5 = new Vector3(hitPos.x + offsetX, hitPos.y + offsetY, 0f);
            pos6 = new Vector3(hitPos.x + offsetX, hitPos.y - offsetY, 0f);
            pos7 = new Vector3(hitPos.x - offsetX, hitPos.y + offsetY, 0f);
            pos8 = new Vector3(hitPos.x - offsetX, hitPos.y - offsetY, 0f);

            Vector3Int position = destructLayer.WorldToCell(pos1);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos2);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos3);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos4);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos5);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos6);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos7);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);
            position = destructLayer.WorldToCell(pos8);//得到cell坐标，就是tilemap中一个一个格子
            destructLayer.SetTile(position, null);

            Destroy(other.gameObject);
        }
    }
}
