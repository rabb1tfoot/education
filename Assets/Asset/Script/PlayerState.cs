using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState instance;

    public int hp;
    public int mp;
    public int Lv;
    public int[] needExp;

    public int currentExp;
    public int currentHp;
    public int currentMp;

    public int atk;
    public int def;

    public int recoverHp;
    public int recoverMp;

    public string dmgSound;

    public float time;
    private float current_time;

    public GameObject prefabFloatingText;
    public GameObject parent;

    public void Hit(int _atk)
    {
        int dmg = _atk;

        if (def >= _atk)
            dmg = 1;
        else
            dmg = _atk - def;

        currentHp -= dmg;

        if (currentHp <= 0)
            Debug.Log("체력이 0 입니다");

        AudioManager.instance.Play(dmgSound);

        Vector3 vector = this.transform.position;
        vector.y += 60;

        GameObject clone = Instantiate(prefabFloatingText, vector, Quaternion.Euler(Vector3.zero));
        clone.GetComponent<FloatingText>().text.text = dmg.ToString();
        clone.GetComponent<FloatingText>().text.color = Color.red;
        clone.GetComponent<FloatingText>().text.fontSize = 25;
        clone.transform.SetParent(parent.transform);
        StopAllCoroutines();
        StartCoroutine(HitCoroutine());

    }

    IEnumerator HitCoroutine()
    {
        Color color = GetComponent<SpriteRenderer>().color;

        for (int i = 0; i < 3; ++i)
        {
            color.a = 0;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
            color.a = 1;
            GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        current_time -= Time.deltaTime;

        if(current_time <=0)
        {
            if(recoverHp > 0)
            {
                if (currentHp + recoverHp < hp - 1)
                    currentHp += recoverHp;
            }
            current_time = time;
        }

    }
}
