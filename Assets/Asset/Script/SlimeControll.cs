using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControll : MovingObject
{

    public float attackDelay; // 공격 딜레이
    public float interMoveWaitTime; // 이동 대기시간
    private float currentWaitTime; //현재대기시간

    public string atkSound;

    private Vector2 playerPos;

    private int random_int;
    private string direction;




    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        currentWaitTime = interMoveWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentWaitTime -= Time.deltaTime;

        if(currentWaitTime < 0)
        {
            currentWaitTime = interMoveWaitTime;

            if (NearPlayer())
            {
                Flip();
                return;
            }

            RandomDir();

            if (base.CheckCollision())
            {
                queue.Clear();
                return;
            }

            base.Move(direction);
        }
    }

    private void Flip()
    {
        Vector3 flip = transform.localScale;
        if (playerPos.x > this.transform.position.x)
            flip.x = -1f;
        else
            flip.x = 1f;
        this.transform.localScale = flip;

        animator.SetTrigger("Attack");
        StartCoroutine(WaitCoroutine());
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(attackDelay);
        AudioManager.instance.Play(atkSound);
        if (NearPlayer())
            PlayerState.instance.Hit(GetComponent<EnemyState>().atk);
    }

    private bool NearPlayer()
    {
        playerPos = PlayerManager.instatnce.transform.position;

        if(Mathf.Abs(playerPos.x - this.transform.position.x) <= speed * walkCount * 1.01f)
        {
            if(Mathf.Abs(playerPos.y - this.transform.position.y) <= speed * walkCount * 0.5f)
            {
                return true;
            }
        }

        if (Mathf.Abs(playerPos.y - this.transform.position.y) <= speed * walkCount * 1.01f)
        {
            if (Mathf.Abs(playerPos.x - this.transform.position.x) <= speed * walkCount * 0.5f)
            {
                return true;
            }
        }

        return false;
    }

    private void RandomDir()
    {
        vector.Set(0, 0, vector.z);
        random_int = Random.Range(0, 4);
        switch(random_int)
        {
            case 0:
                vector.y = 1f;
                direction = "UP";
                break;

            case 1:
                vector.y = -1f;
                direction = "DOWN";
                break;

            case 2:
                vector.x = 1f;
                direction = "RIGHT";
                break;

            case 3:
                vector.x = -1f;
                direction = "LEFT";
                break;
        }
    }
}
