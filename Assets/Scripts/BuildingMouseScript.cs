using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMouseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f;
    public float rotateSpeed;
    public Vector3 targetPos;
    public bool isMoving;
    const int MOUSE = 0;
    [SerializeField] LayerMask mask;

    void Start()
    {

        targetPos = transform.position;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetMouseButton(MOUSE))
        //{
        //    SetTarggetPosition();
        //}
        //if (isMoving)
        //{

        //}

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Rotate(Vector3.up * rotateSpeed, Space.Self);
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Rotate(Vector3.down * rotateSpeed, Space.Self);
        }


        SetTarggetPosition();
        MoveObject();
    }
    void SetTarggetPosition()
    {
        Plane plane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float point = 0f;

        Ray rayray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rayray, out hit, 10000, mask))
        {
            if (hit.collider.gameObject.layer == 6)
            {
                if (plane.Raycast(ray, out point))
                    targetPos = ray.GetPoint(point);

                isMoving = true;
            }
        }


        
    }
    void MoveObject()
    {
        //transform.LookAt(targetPos);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        if (transform.position == targetPos)
            isMoving = false;
        Debug.DrawLine(transform.position, targetPos, Color.red);

    }
}
