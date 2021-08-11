using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerPhysics2DAndMecanim : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator anim;

    public float speed = 12f;
    public float jumpPower = 500f;

    bool grounded;  // 접지 체크
    bool goalCheck;
    float goalTime;

    void Start()
    {
        grounded = false;
        goalCheck = false;
        rigid2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
                rigid2D.AddForce(new Vector2(0, jumpPower));
            }
            anim.SetTrigger("Run");
        }
        else
        {
            anim.SetTrigger("Jump");
        }
        if (transform.position.y < -10f)
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
