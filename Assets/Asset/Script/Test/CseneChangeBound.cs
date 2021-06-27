using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CseneChangeBound : MonoBehaviour
{
    // Start is called before the first frame update
    private BoxCollider2D bound;
    public string boundName;
    private CameraManager Camera;
    void Start()
    {
        bound = GetComponent<BoxCollider2D>();
        Camera = FindObjectOfType<CameraManager>();
        Camera.SetBound(bound);
    }

    public void SetBound()
    {
        if(Camera != null)
        {
            Camera.SetBound(bound);
        }
    }
}
