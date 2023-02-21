using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int Blinks;
    public float time;
    public float HixBoxCdTime;

    private const float dieTime = 0.833f;
    private Renderer myRender;
    private Animator anim;
    private ScreenFlash sf;
    private PolygonCollider2D polygonCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        HealthBar.HealthMax = health;
        HealthBar.HealthCurrent = health;
        myRender = GetComponent<Renderer>();
        anim = GetComponent<Animator>();
        sf = GetComponent<ScreenFlash>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        sf.FlashScreen();
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        HealthBar.HealthCurrent = health;
        if (health <= 0)
        {
            GameController.isGameAlive = false;
            anim.SetTrigger("Die");
            Invoke("KillPlayer",dieTime);
        }
        BlinkPlayer(Blinks, time);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHixBox());
    }

    IEnumerator ShowPlayerHixBox()
    {
        yield return new WaitForSeconds(HixBoxCdTime);
        polygonCollider2D.enabled = true; ;
    }

    void KillPlayer()
    {
        Destroy(gameObject);
    }

    void BlinkPlayer(int numBlink, float seconds)
    {
        StartCoroutine(DoBlinks(numBlink, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for (int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRender.enabled = true;
    }
}
