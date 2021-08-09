using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNonPhysics2D : MonoBehaviour
{
    // Inspector���� ����
    public float speed = 15.0f; // �÷��̾� �ӵ�
    public Sprite[] run;    // �÷��̾� �޸��� ��������Ʈ
    public Sprite[] jump;   // �÷��̾� ���� ��������Ʈ

    // -- //
    float jumpVy;   // �÷��̾� ���� ��� �ӵ�
    int animIndex;  // �÷��̾� �ִϸ��̼� ��� �ε���
    bool goalCheck; // ���� üũ

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
            return; // ���̸� ó���� ����
        }

        // �÷��̾� ���� ���� ���
        float height = transform.position.y + jumpVy;
        // ���� Ȯ��(���̰� 0�̸� ����)
        if (height <= 0)
        {
            // ���� �ʱ�ȭ
            height = 0;
            jumpVy = 0;

            // ���� Ȯ��
            if (Input.GetMouseButtonDown(0))
            {
                // ���� ó��
                jumpVy = +1.3f;
                // ���� ��������Ʈ �̹��� ��ȯ
                GetComponent<SpriteRenderer>().sprite = jump[0];
            }
            else
            {
                // �޸��� ó��
                animIndex++;
                if(animIndex > 3)
                {
                    animIndex = 0;
                }
                // �޸��� ��������Ʈ �̹��� ��ȯ
                GetComponent<SpriteRenderer>().sprite = run[animIndex];
            }
        }
        else
        {
            // ���� �� �������� ����
            jumpVy -= 6f * Time.deltaTime;
        }

        // �÷��̾� ĳ���� �̵�
        // transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, height, 0);
        transform.Translate(speed * Time.deltaTime, jumpVy, 0);

        // ī�޶� �̵�
        GameObject Cam = GameObject.Find("Main Camera");
        Cam.transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnGUI()
    {
        // �����
        GUI.TextField(new Rect(10, 10, 300, 60), "[SampleRunner]\n���콺 ���� ��ư�� ������ ����\n������ ����");
        // ���� ��ư
        if(GUI.Button(new Rect(10, 80, 100, 20), "����"))
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
        // �� ������ �ƴ϶�� �������� �ٽ� �ε� �� �ʱ�ȭ
        SceneManager.LoadScene("StageA");
    }
}
