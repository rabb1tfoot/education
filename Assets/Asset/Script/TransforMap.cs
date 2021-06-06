using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransforMap : MonoBehaviour
{
    public Transform target;
    public string direction;
    private PlayerManager Player;
    private CameraManager Camera;
    public BoxCollider2D targetBound;

    void Start()
    {
        Player = FindObjectOfType<PlayerManager>();
        Camera = FindObjectOfType<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Camera.SetBound(targetBound);
            if (direction.Equals("Up"))
            {

                Player.transform.position = target.transform.position;

                Camera.transform.position = new Vector3(target.transform.position.x
                    , target.transform.position.y  +48, Camera.transform.position.z);
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
        }

    }
}
