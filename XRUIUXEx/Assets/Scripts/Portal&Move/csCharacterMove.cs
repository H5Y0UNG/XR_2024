using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csCharacterMove : MonoBehaviour
{
    public float speed = 5.0f; // 캐릭터의 이동 속도
    public float jumpHeight = 1.0f; // 점프 높이
    public float gravityValue = -9.81f; // 중력 값

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private CapsuleCollider capsuleCollider; // 추가된 코드

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        capsuleCollider = gameObject.AddComponent<CapsuleCollider>(); // 추가된 코드
        capsuleCollider.radius = 0.5f; // 캡슐 콜라이더의 반지름
        capsuleCollider.height = 1.8f; // 캡슐 콜라이더의 높이
        capsuleCollider.center = new Vector3(0, 0.9f, 0); // 캡슐 콜라이더의 중심
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move); // 이동 방향을 캐릭터의 방향으로 변환
        move *= speed; // 이동 속도 적용

        // 경사면의 법선 방향으로 이동 방향을 조정
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
        {
            Vector3 slopeNormal = hit.normal;
            move = Vector3.ProjectOnPlane(move, slopeNormal);
        }

        // 캐릭터가 주변 사물에 부딪혀서 통과하지 않도록 하는 코드
        if (Physics.CheckCapsule(capsuleCollider.bounds.min, capsuleCollider.bounds.max, capsuleCollider.radius))
        {
            move = Vector3.zero; // 캐릭터가 부딪혔을 때 이동 방향을 0으로 설정
        }

        controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // 점프 처리
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}