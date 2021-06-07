using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("mpc를 체크하면 npc가움직임")]
    public bool NPCmove;

    public string[] direction; //npc방향설정

    [Range(1,5)][Tooltip("클수록 빠르게")]
    public int frequency; // npc가 움직일 방향으로 어느속도로 움직일것인가
}

public class NPCManager : MovingObject
{
    public NPCMove npc;

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void SetMove()
    {
        StartCoroutine(MoveCoroutine());
    }
    public void SetNotMove()
    {
        StopCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        if(npc.direction.Length != 0)
        {
            for(int i = 0; i < npc.direction.Length; ++i)
            {
                
                //대기 종료후 이동
                yield return new WaitUntil(() => queue.Count < 2);               
                base.Move(npc.direction[i], npc.frequency);

                if(i == npc.direction.Length -1)
                {
                    i = -1;
                }
            }
        }
    }
}
