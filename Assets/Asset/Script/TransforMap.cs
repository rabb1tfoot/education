using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransforMap : MonoBehaviour
{
    public string transferMapName;

    public Transform target;
    public string direction;
    private PlayerManager Player;
    private CameraManager Camera;

    void Start()
    {
        Player = FindObjectOfType<PlayerManager>();
        Camera = FindObjectOfType<CameraManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Player.currentMapName = transferMapName;

            Player.transform.position = target.transform.position;

            Camera.transform.position = new Vector3(target.transform.position.x
                , target.transform.position.y, this.transform.position.z);
        }
    }
}
