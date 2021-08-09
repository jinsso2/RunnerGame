using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNonPhysics2D : MonoBehaviour
{
    // Inspector에서 조정
    public float speed = 15.0f; // 플레이어 속도
    public Sprite[] run;    // 플레이어 달리기 스프라이트
    public Sprite[] jump;   // 플레이어 점프 스프라이트

    // -- //
    float jumpVy;   // 플레이어 점프 상승 속도
    int animIndex;  // 플레이어 애니메이션 재생 인덱스
    bool goalCheck; // 골인 체크

    void Start()
    {
        jumpVy = 0.0f;
        animIndex = 0;
        goalCheck = false;

    }

    void Update()
    {
        if (goalCheck)
        {
            return; // 참이면 처리를 멈춤
        }

        // 플레이어 현재 높이 계산
        float height = transform.position.y + jumpVy;
        // 접지 확인(높이가 0이면 접지)
        if (height <= 0)
        {
            // 점프 초기화
            height = 0;
            jumpVy = 0;

            // 점프 확인
            if (Input.GetMouseButtonDown(0))
            {
                // 점프 처리
                jumpVy = +1.3f;
                // 점프 스프라이트 이미지 전환
                GetComponent<SpriteRenderer>().sprite = jump[0];
            }
            else
            {
                // 달리기 처리
                animIndex++;
                if(animIndex > 3)
                {
                    animIndex = 0;
                }
                // 달리기 스프라이트 이미졸 전환
                GetComponent<SpriteRenderer>().sprite = run[animIndex];
            }
        }
        else
        {
            // 점프 후 떨어지는 도중
            jumpVy -= 6f * Time.deltaTime;
        }

        // 플레이어 캐릭터 이동
        // transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, height, 0);
        transform.Translate(speed * Time.deltaTime, jumpVy, 0);

        // 카메라 이동
        GameObject Cam = GameObject.Find("Main Camera");
        Cam.transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnGUI()
    {
        // 디버그
        GUI.TextField(new Rect(10, 10, 300, 60), "[SampleRunner]\n마우스 왼쪽 버튼을 누르면 가속\n놓으면 점프");
        // 리셋 버튼
        if(GUI.Button(new Rect(10, 80, 100, 20), "리셋"))
        {
            SceneManager.LoadScene("StageA");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Stage_Gate")
        {
            goalCheck = true;
        }
        // 골 지점이 아니라면 스테이지 다시 로딩 후 초기화
        SceneManager.LoadScene("StageA");
    }
}
