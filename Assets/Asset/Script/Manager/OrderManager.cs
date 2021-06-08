using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    private PlayerManager Player; //이동중에 키입력 처리 방지
    private List<MovingObject> characters;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerManager>();
    }
    public void PreLoadCharactor()
    {
        characters = ToList();
    }

    public void NoMove()
    {
        Player.notMovewhentalking = true;
    }

    public void CanMove()
    {
        Player.notMovewhentalking = false;
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();

        for(int i = 0; i < temp.Length; ++i)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void SetTransparent(string _name)
    {
        for(int i = 0; i < characters.Count; ++i)
        {
            if(_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    }
    public void SetUnTransparent(string _name)
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            if (_name == characters[i].characterName)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }

    public void Move(string _name, string _dir)
    {
        for(int i = 0; i < characters.Count; ++i)
        {
            if(characters[i].characterName == _name)
            {
                characters[i].Move(_dir);
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; ++i)
        {
            if (characters[i].characterName == _name)
            {

                characters[i].animator.SetFloat("DirX", 0f);
                characters[i].animator.SetFloat("DirY", 0f);
                switch (_dir)
                {
                    case "UP":
                        characters[i].animator.SetFloat("DirY", 1f);
                        break;
                    case "DOWN":
                        characters[i].animator.SetFloat("DirY", -1f);
                        break;
                    case "LEFT":
                        characters[i].animator.SetFloat("DirX", -1f);
                        break;
                    case "RIGHT":
                        characters[i].animator.SetFloat("DirX", 1f);
                        break;

                }
            }
        }
    }
}
