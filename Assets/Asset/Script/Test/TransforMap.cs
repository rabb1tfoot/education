using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransforMap : MonoBehaviour
{
    public Transform target;
    public string direction;
    public BoxCollider2D targetBound;

    private MovingObject Player;
    private CameraManager Camera;
    private FadeManager Fade;
    private OrderManager Order;
    void Start()
    {
        Player = FindObjectOfType<MovingObject>();
        Camera = FindObjectOfType<CameraManager>();
        Fade = FindObjectOfType<FadeManager>();
        Order = FindObjectOfType<OrderManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(MapTransforCoroutine());
        }

    }

    IEnumerator MapTransforCoroutine()
    {
        Fade.FadeOut();
        Order.NoMove();
        yield return new WaitForSeconds(1f);

        Camera.SetBound(targetBound);
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
