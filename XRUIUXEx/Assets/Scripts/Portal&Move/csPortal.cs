using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPortal : MonoBehaviour
{
    public GameObject exitPortal; // 출구 포탈
    public GameObject entrance; // 입구 포탈

    private void OnTriggerEnter(Collider other)
    {
        // 유저의 콜라이더가 입구 포탈에 충돌하면
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enterd");
            // 유저를 출구 포탈로 이동
            other.transform.position = exitPortal.transform.position;
        }
    }
}
