using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    // Start is called before the first frame update

    static public PlayerManager instatnce;

    public string currentMapName; //���� ���̸�

    public string walksound_1;
    public string walksound_2;

    private AudioManager customaudio;

    public float runspeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    private int currentWalkCount;

    private bool canMove = true;

    void Start()
    {
        if (instatnce == null)
        {
            instatnce = this;
            DontDestroyOnLoad(this.gameObject);
            animator = GetComponent<Animator>();
            boxCollider = GetComponent<BoxCollider2D>();
            customaudio = FindObjectOfType<AudioManager>();

        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 ||
            Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runspeed;
                applyRunFlag = true;
            }

            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            //�ѹ������θ� �����δ�.
            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            RaycastHit2D hit;

            Vector2 start = transform.position; // ĳ���� ������ġ
            Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);
            boxCollider.enabled = false;
            hit = Physics2D.Linecast(start, end, layerMask);
            boxCollider.enabled = true;

            //�浹�ȴٸ� while�� ��������
            if (hit.transform != null) //transform ��� collider2D
                break;

            animator.SetBool("Walking", true);

            //�߰��� ���� �߰�
            int temp = Random.Range(1, 2);

            switch (temp)
            {

                case 1:
                    customaudio.Play(walksound_1);
                    break;
                case 2:
                    customaudio.Play(walksound_1);
                    break;
            }


            while (currentWalkCount < walkCount)
            {
                if (vector.x != 0)
                {
                    transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (vector.y != 0)
                {
                    transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                {
                    currentWalkCount++;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);

            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 ||
                Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
