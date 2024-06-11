using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCharacterMove : MonoBehaviour
{
    public float speed = 5.0f; // ĳ������ �̵� �ӵ�
    public float jumpHeight = 1.0f; // ���� ����
    public float gravityValue = -9.81f; // �߷� ��

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private CapsuleCollider capsuleCollider; // �߰��� �ڵ�

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        capsuleCollider = gameObject.AddComponent<CapsuleCollider>(); // �߰��� �ڵ�
        capsuleCollider.radius = 0.5f; // ĸ�� �ݶ��̴��� ������
        capsuleCollider.height = 1.8f; // ĸ�� �ݶ��̴��� ����
        capsuleCollider.center = new Vector3(0, 0.9f, 0); // ĸ�� �ݶ��̴��� �߽�
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move); // �̵� ������ ĳ������ �������� ��ȯ
        move *= speed; // �̵� �ӵ� ����

        // ������ ���� �������� �̵� ������ ����
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            Vector3 slopeNormal = hit.normal;
            move = Vector3.ProjectOnPlane(move, slopeNormal);
        }

        // ĳ���Ͱ� �ֺ� �繰�� �ε����� ������� �ʵ��� �ϴ� �ڵ�
        if (Physics.CheckCapsule(capsuleCollider.bounds.min, capsuleCollider.bounds.max, capsuleCollider.radius))
        {
            move = Vector3.zero; // ĳ���Ͱ� �ε����� �� �̵� ������ 0���� ����
        }

        controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // ���� ó��
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}