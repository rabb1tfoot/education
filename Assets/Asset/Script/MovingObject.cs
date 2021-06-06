using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;

    public float speed;
    public int walkCount;

    private bool notCoroutine = false;

    protected Vector3 vector;
    protected int currentWalkCount;

    public Queue<string> queue;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;
    public Animator animator;

    private void Start()
    {
    }

    public void Move(string _dir, int frequency = 5)
    {
        queue.Enqueue(_dir);
        if(!notCoroutine)
        {
            notCoroutine = true;
            StartCoroutine(MoveCoroutine(_dir, frequency));
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while(queue.Count != 0)
        {
            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            animator.SetBool("Walking", true);

            while (currentWalkCount < walkCount)
            {

                transform.Translate(vector.x * (speed), vector.y * (speed), 0);

                currentWalkCount++;
                yield return new WaitForSeconds(0.02f);

            }
            currentWalkCount = 0;
            if (_frequency != 5)
            {
                animator.SetBool("Walking", false);
            }
        }
        animator.SetBool("Walking", false);
        notCoroutine = false;

    }
    protected bool CheckCollision()
    {
        RaycastHit2D hit;

        Vector2 start = transform.position; // 캐릭터 현재위치
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        //충돌된다면 while을 빠져나감
        if (hit.transform != null) //transform 대신 collider2D
            return true;

        return false;
    }

}
