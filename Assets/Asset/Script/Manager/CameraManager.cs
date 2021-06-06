using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update

    static public CameraManager instance;

    public GameObject target;
    public float moveSpeed;
    private Vector3 targetPosition;

    public BoxCollider2D cameraBound;

    public Vector3 minBound;
    public Vector3 maxBound;

    private float halfWidth;
    private float halfHeight;

    private Camera Camera;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        Camera = this.GetComponent<Camera>();
        minBound = cameraBound.bounds.min;
        maxBound = cameraBound.bounds.max;
        halfHeight = Camera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height;
    }

    // Update is called once per frame
    void Update()
    {
        if(target.gameObject != null)
        {
            targetPosition.Set(target.transform.position.x
                , target.transform.position.y, this.transform.position.z);

            //Lerp 부드럽게 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            //영역외 이동못하게 막음
            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D _bound)
    {
        cameraBound = _bound;
        minBound = cameraBound.bounds.min;
        maxBound = cameraBound.bounds.max;
    }
}
