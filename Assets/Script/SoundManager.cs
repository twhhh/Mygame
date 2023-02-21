using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;//音源
    public static AudioClip pickCoin;
    public static AudioClip throwCoin;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");//名字一定不能错，根据名字找音乐片
        throwCoin = Resources.Load<AudioClip>("ThrowCoin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayPickCoinClip()
    {
        audioSrc.PlayOneShot(pickCoin);//播放一次
    }

    public static void PlayPickThrowCoin()
    {
        audioSrc.PlayOneShot(throwCoin);
    }
}
