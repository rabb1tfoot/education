using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCMove
{
    [Tooltip("mpc�� üũ�ϸ� npc��������")]
    public bool NPCmove;

    public string[] direction; //npc���⼳��

    [Range(1,5)][Tooltip("Ŭ���� ������")]
    public int frequency; // npc�� ������ �������� ����ӵ��� �����ϰ��ΰ�
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
                
                //��� ������ �̵�
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
