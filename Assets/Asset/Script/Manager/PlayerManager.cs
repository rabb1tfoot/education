using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MovingObject
{
    // Start is called before the first frame update

    static public PlayerManager instatnce;

    public string currentMapName; //현재 맵이름

    public string walksound_1;
    public string walksound_2;

    private AudioManager customaudio;

    public float runspeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;

    private bool canMove = true;
    public bool notMovewhentalking = false;
    private float animSpeed = 1f;

    private bool attacking = false;

    public float attackDelay;
    private float currentAttackDelay;

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

        string Path = Application.streamingAssetsPath;
        string[] pathsplit = Path.Split('/');
        Path = "";
        for (int i = 0; i < pathsplit.Length - 1; ++i)
        {
            Path += pathsplit[i];
            Path += "/";
        }
        Path += "\\Asset\\ItemIcon";

    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 ||
            Input.GetAxisRaw("Horizontal") != 0 && !notMovewhentalking && !attacking)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animSpeed = 2f;
                animator.SetFloat("AnimSpeed", animSpeed);
                applyRunSpeed = runspeed;
                applyRunFlag = true;
            }

            else
            {
                animSpeed = 1f;
                animator.SetFloat("AnimSpeed", animSpeed);
                applyRunSpeed = 0;
                applyRunFlag = false;
            }


            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            //한방향으로만 움직인다.
            if (vector.x != 0)
                vector.y = 0;

            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount
  , vector.y * 0.7f * speed * walkCount);

            bool checkCollisionFlag = base.CheckCollision();
            if (checkCollisionFlag)
                break;

            animator.SetBool("Walking", true);

            //발걸음 사운드 추가
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
                if(currentWalkCount == 12)
                {
                    boxCollider.offset = Vector2.zero;
                }
                currentWalkCount++;
                yield return new WaitForSeconds(0.015f);

            }
            currentWalkCount = 0;
        }
        animator.SetBool("Walking", false);
        canMove = true;

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove && !notMovewhentalking && !attacking)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 ||
                Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }

        if(!notMovewhentalking && !attacking)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                currentAttackDelay = attackDelay;
                attacking = true;
                animator.SetBool("Attacking", true);

            }
        }

        if(attacking)
        {
            currentAttackDelay -= Time.deltaTime;
            if(currentAttackDelay < 0)
            {
                animator.SetBool("Attacking", false);
                attacking = false;
            }
        }
    }
}
