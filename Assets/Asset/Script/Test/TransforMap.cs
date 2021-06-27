using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransforMap : MonoBehaviour
{
    public Transform target;
    [Tooltip("Up, Down, Left, Right")]
    public string direction;
    public BoxCollider2D targetBound;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;
    private Vector2 vector;

    [Tooltip("문의 존재여부(애니메이션) true or false")]
    public bool door;

    private MovingObject Player;
    private CameraManager Camera;
    private FadeManager Fade;
    private OrderManager Order;
    public string changeMapName;
    void Start()
    {
        Player = FindObjectOfType<MovingObject>();
        Camera = FindObjectOfType<CameraManager>();
        Fade = FindObjectOfType<FadeManager>();
        Order = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!door)
        {
            if (collision.gameObject.name == "Player")
            {
                StartCoroutine(MapTransforCoroutine());
            }
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(door)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                vector.Set(Player.animator.GetFloat("DirX"), Player.animator.GetFloat("DirY"));
                switch (direction)
                {
                    case "Up":
                        if(vector.y == 1f)
                        {
                            StartCoroutine(MapTransforCoroutine());
                        }
                        break;
                    case "Down":
                        if (vector.y == -1f)
                        {
                            StartCoroutine(MapTransforCoroutine());
                        }
                        break;
                    case "Right":
                        if (vector.x == 1f)
                        {
                            StartCoroutine(MapTransforCoroutine());
                        }
                        break;
                    case "Left":
                        if (vector.x == -1f)
                        {
                            StartCoroutine(MapTransforCoroutine());
                        }
                        break;
                    default:
                        StartCoroutine(MapTransforCoroutine());
                        break;
                }
            }
        }
    }

    IEnumerator MapTransforCoroutine()
    {
        Order.PreLoadCharactor();

        Fade.FadeOut();
        Order.NoMove();

        if(door)
        {
            anim_1.SetBool("Open", true);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", true);
            }
        }

        yield return new WaitForSeconds(0.5f);

        Order.SetTransparent("Player");

        if(door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", false);
            }
        }

        yield return new WaitForSeconds(0.5f);
        Order.SetUnTransparent("Player");
        Camera.SetBound(targetBound);
        PlayerManager.instatnce.currentMapName = changeMapName;
        if (direction.Equals("Up"))
        {

            Player.transform.position = target.transform.position;

            Camera.transform.position = new Vector3(target.transform.position.x
                , target.transform.position.y + 48, Camera.transform.position.z);
        }
        else if (direction.Equals("Down"))
        {

            Player.transform.position = target.transform.position;

            Camera.transform.position = new Vector3(target.transform.position.x
                , target.transform.position.y - 48, Camera.transform.position.z);
        }
        else if (direction.Equals("Left"))
        {

            Player.transform.position = target.transform.position;

            Camera.transform.position = new Vector3(target.transform.position.x - 48
                , target.transform.position.y, Camera.transform.position.z);
        }
        else if (direction.Equals("Right"))
        {

            Player.transform.position = target.transform.position;

            Camera.transform.position = new Vector3(target.transform.position.x + 48
                , target.transform.position.y, Camera.transform.position.z);
        }
        else
        {
            Player.transform.position = target.transform.position;

            Camera.transform.position = new Vector3(target.transform.position.x
                , target.transform.position.y, Camera.transform.position.z);
        }
        Fade.FadeIn();
        Order.CanMove();
    }
}
