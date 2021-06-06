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

    private bool canMove = true;

    private void Awake()
    {
        if (instatnce == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instatnce = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        queue = new Queue<string>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        customaudio = FindObjectOfType<AudioManager>();
        currentWalkCount = 0;

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

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag)
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
                    customaudio.Play(walksound_2);
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
                yield return new WaitForSeconds(0.02f);

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
