using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Transform ballista;

    [Header("Ballista Behaviour")]
    [SerializeField, Range(0, -100)] private int minRot;
    [SerializeField, Range(0, 100)] private int maxRot;

    [SerializeField, Range(0.0f, 50.0f)] private float speed = 45.0f;
    [SerializeField, Range(0.0f, 50.0f)] private float deltaAngle = 45.0f;

    public Transform cannon;
    public Transform spawnPoint;



    private bool shoot;

    public bool Shoot { get { return shoot; } }


[Header("Extra")]
    [SerializeField] private bool lockMouse = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //Time.timeScale = 0.2f;
    }






    private void Update()
    {
        Movement();


        Fire();
    }


    private void LateUpdate()
    {
        CheckCursorBehaviour();
    }


    private void Movement()
    {
        float input = Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X");

        input = Mathf.Clamp(input, -1.0f, 1.0f);

        float rotAmount = -input * deltaAngle;

        Quaternion currentRot = ballista.rotation;

        float currentAngle = (currentRot.eulerAngles.x > 180) ? currentRot.eulerAngles.x - 360 : currentRot.eulerAngles.x;

        float newRot = Mathf.Clamp(currentAngle + rotAmount, minRot, maxRot);


        Quaternion newQuat = Quaternion.Euler(newRot, 0, 0);
        ballista.transform.rotation = Quaternion.Slerp(currentRot, newQuat, Time.deltaTime * speed);
    }

    private void Fire()
    {
       shoot = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);
        
        
        if(shoot)
        {
            Transform newCannon = Instantiate(cannon, spawnPoint.position, transform.rotation);

            if (newCannon.TryGetComponent<CannonBehaviour>(out CannonBehaviour behaviour))
            {
                behaviour.ForceDirection(ballista.transform.up);
            }
        }
    }


    private void CheckCursorBehaviour()
    {
        if(lockMouse)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

}
