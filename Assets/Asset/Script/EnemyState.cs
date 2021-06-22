using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{

    public int hp;
    public int currentHp;
    public int atk;
    public int def;
    public int exp;

    void Start()
    {
        currentHp = hp;
    }

    public int Hit(int _playerAtk)
    {
        int dmg;
        if (def >= _playerAtk)
            dmg = 1;
        else
            dmg = _playerAtk - def;

        currentHp -= dmg;

        if(currentHp <= 0)
        {
            Destroy(this.gameObject);
            PlayerState.instance.currentExp += exp;
        }

        return dmg;
    }
}
