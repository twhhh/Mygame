using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject coin;
    public int coinQuantity;
    public float coinUpSpeed;
    public float intervalTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenCoins());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator GenCoins()
    {
        for (int i = 0; i < coinQuantity; i++)
        {
            GameObject gb = Instantiate(coin, transform.position, Quaternion.identity);
            Vector2 randomDirection = new Vector2(Random.Range(-0.3f, 0.3f), 1.0f);
            gb.GetComponent<Rigidbody2D>().velocity = randomDirection * coinUpSpeed;
            yield return new WaitForSeconds(intervalTime);
        }
    }
}
