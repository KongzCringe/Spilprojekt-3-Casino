using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Header("Modules")] 
    
    [SerializeField] private Transform dolley;
    
    [Header("Points")]
    
    [SerializeField] private Transform highPoint;
    [SerializeField] private Transform midPoint1;
    [SerializeField] private Transform midPoint2;
    [SerializeField] private Transform lowPoint;

    [Range(0f, 1f)] [SerializeField] private float fractionOfJourney;

    [SerializeField] private Vector4 bounds;
    
    [SerializeField] private Vector3 calculatedPosition;
    [SerializeField] private Vector3 calculatedEulerAngles;
    [SerializeField] private Vector3 calculatedDolleyPos;
    [SerializeField] private float calculatedFracOfJour;

    [SerializeField] private float speed = 1000;
    private float startSpeed = 1000;

    private void Update()
    {
        calculatedFracOfJour += Input.GetAxis("Mouse ScrollWheel");
        calculatedFracOfJour = Mathf.Clamp(calculatedFracOfJour, 0f, 1f);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = startSpeed * 1.75f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            speed = startSpeed / 1.75f;
        }

        calculatedDolleyPos += new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) * speed * Time.deltaTime;

        fractionOfJourney = Mathf.Lerp(fractionOfJourney, calculatedFracOfJour, 8f * Time.deltaTime);

        calculatedPosition = Bezier(highPoint.position, midPoint1.position, midPoint2.position, lowPoint.position, fractionOfJourney);
        calculatedEulerAngles = Bezier(highPoint.localEulerAngles, midPoint1.localEulerAngles,
            midPoint2.localEulerAngles, lowPoint.localEulerAngles, fractionOfJourney);
        
        
        
        transform.position = calculatedPosition;
        transform.localEulerAngles = calculatedEulerAngles;
        calculatedDolleyPos = new Vector3(Mathf.Clamp(calculatedDolleyPos.x, bounds.x, bounds.y), 0,
            Mathf.Clamp(calculatedDolleyPos.z, bounds.z, bounds.w));
        dolley.position = calculatedDolleyPos;
    }

    private Vector3 Bezier(Vector3 a, Vector3 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }
    private Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        return Vector3.Lerp(Bezier(a,b,t), Bezier(b,c,t), t);
    }

    private Vector3 Bezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        return Vector3.Lerp(Bezier(a, b, c, t), Bezier(b, c, d, t), t);
    }
    
}
