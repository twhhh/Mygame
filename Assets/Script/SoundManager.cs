using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource audioSrc;//��Դ
    public static AudioClip pickCoin;
    public static AudioClip throwCoin;

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pickCoin = Resources.Load<AudioClip>("PickCoin");//����һ�����ܴ���������������Ƭ
        throwCoin = Resources.Load<AudioClip>("ThrowCoin");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayPickCoinClip()
    {
        audioSrc.PlayOneShot(pickCoin);//����һ��
    }

    public static void PlayPickThrowCoin()
    {
        audioSrc.PlayOneShot(throwCoin);
    }
}
