using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPhysics2D : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public float speed = 12f;
    public float jumpPower = 500f;
    public Sprite[] run;
    public Sprite[] jump;

    int animIndex;
    bool grounded;  // 접지 체크
    bool goalCheck;
    float goalTime;

    void Start()
    {
        animIndex = 0;
        grounded = false;
        goalCheck = false;
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Transform groundCheck = transform.Find("GroundCheck");
        grounded = (Physics2D.OverlapPoint(groundCheck.position) != null) ? true : false;

        if (grounded)
        {
            // 점프
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpPower));
                GetComponent<SpriteRenderer>().sprite = jump[0];
            }
            else
            {
                // 달리기
                animIndex++;
                if(animIndex > run.Length)
                {
                    animIndex = 0;
                }
                GetComponent<SpriteRenderer>().sprite = run[animIndex];
            }
        }
        if(transform.position.y < -10f)
        {
            SceneManager.LoadScene("StageB");
        }
    }

    private void FixedUpdate()
    {
        rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);
        // 카메라 이동
        GameObject Cam = GameObject.Find("Main Camera");
        Cam.transform.position = new Vector3(transform.position.x + 5f, Cam.transform.position.y, Cam.transform.position.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Stage_Gate")
        {
            goalCheck = true;
            goalTime = Time.timeSinceLevelLoad;
        }
    }
}
