using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csPortal : MonoBehaviour
{
    public GameObject exitPortal; // �ⱸ ��Ż
    public GameObject entrance; // �Ա� ��Ż

    private void OnTriggerEnter(Collider other)
    {
        // ������ �ݶ��̴��� �Ա� ��Ż�� �浹�ϸ�
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enterd");
            // ������ �ⱸ ��Ż�� �̵�
            other.transform.position = exitPortal.transform.position;
        }
    }
}
