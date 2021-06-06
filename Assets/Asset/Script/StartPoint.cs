using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // Start is called before the first frame update

    public string startPoint;
    private MovingObject Player;
    private CameraManager Camera;

    void Start()
    {
        Player = FindObjectOfType<MovingObject>();
        Camera = FindObjectOfType<CameraManager>();
        if(startPoint == Player.currentMapName)
        {
            Player.transform.position = this.transform.position;
            Camera.transform.position = new Vector3(this.transform.position.x
                , this.transform.position.y, Camera.transform.position.z);
        }
    }
}
