using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBGM : MonoBehaviour
{
    public GameObject uiObject;

    public AudioClip start;
    public AudioClip house;
    public AudioClip city;
    public AudioClip forest;

    private AudioSource audioSource;

    public Material citySkybox;
    public Material forestSkybox;

    private void Start()
    {
        // AudioSource 컴포넌트 가져오기
        audioSource = GetComponent<AudioSource>();

        // 처음 시작할 때 오디오 A 재생
        audioSource.clip = start;
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 객체가 플레이어인지 체크1인지 확인
        if (other.gameObject.name=="intoHouse")
        {
            // 오디오 A 멈추고 오디오 B 재생
            //RenderSettings.skybox = Sky;
            audioSource.Stop();
            audioSource.clip = house;
            audioSource.Play();
            uiObject.GetComponent<UIScript>().ChangeTheme("House");
        }
        if (other.gameObject.name == "intoForest")
        {
            // 오디오 A 멈추고 오디오 B 재생
            RenderSettings.skybox = forestSkybox;
            audioSource.Stop();
            audioSource.clip = forest;
            audioSource.Play();
            uiObject.GetComponent<UIScript>().ChangeTheme("Forest");
        }
        if (other.gameObject.name == "intoCity")
        {
            // 오디오 A 멈추고 오디오 B 재생
            RenderSettings.skybox = citySkybox;
            audioSource.Stop();
            audioSource.clip = city;
            audioSource.Play();
            uiObject.GetComponent<UIScript>().ChangeTheme("City");
        }
    }
}
