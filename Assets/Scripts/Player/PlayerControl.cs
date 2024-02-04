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

    [SerializeField, Range(0.0f, 1.5f)]float timerRequired = 1.0f;
    [SerializeField] bool shot = false;
    [SerializeField] float pressedTime = 0.0f;

    [SerializeField] private SkinnedMeshRenderer ballistaMesh;

    private bool shoot;


    public bool Shoot { get { return shoot; } }


    [Header("Extra")]
    [SerializeField] private bool lockMouse = false;

    public bool beingPulled = false;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;

        //Time.timeScale = 0.2f;
    }






    private void Update()
    {
        Movement();
        //Fire();

        NewFire();
        BallistaVisuals();

        //testing
        //if (Input.GetKeyDown(KeyCode.Space) && pressedTime <= 0f || Input.GetMouseButtonDown(0))
        //{
        //    FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.BowPull, transform.position, 0.4f);
        //}

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.BowPull, transform.position, 0.7f);
        }
    }


    private void LateUpdate()
    {
        //CheckCursorBehaviour();
    }


    private void Movement()
    {
        float input = Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X");

        input = Mathf.Clamp(input, -1.0f, 1.0f);

        float rotAmount = -input * deltaAngle;

        Quaternion currentRot = ballista.localRotation;


        float currentAngle = (currentRot.eulerAngles.x > 180) ? currentRot.eulerAngles.x - 360 : currentRot.eulerAngles.x;

        float newRot = Mathf.Clamp(currentAngle + rotAmount, minRot, maxRot);


        Quaternion newQuat = Quaternion.Euler(newRot, 0.0f, 0.0f);
        ballista.localRotation = Quaternion.Slerp(currentRot, newQuat, Time.deltaTime * speed);
    }

    private void Fire()
    {

        Transform newCannon = Instantiate(cannon, spawnPoint.position, ballista.rotation);

        if (newCannon.TryGetComponent<CannonBehaviour>(out CannonBehaviour behaviour))
        {

            behaviour.ForceDirection(ballista.transform.up);
            FindObjectOfType<AudioManager>().AudioTrigger(AudioManager.SoundFXCat.ArrowShot, transform.position, 1f);
            Vector3 pos1 = ballista.transform.position + (Quaternion.Euler(0, 5.0f, 0) * ballista.transform.up);
            Vector3 pos2 = ballista.transform.position + (Quaternion.Euler(0, -5.0f, 0) * ballista.transform.up);

           
        }
        //shoot = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);


        //if(shoot)
        //{
        //    Transform newCannon = Instantiate(cannon, spawnPoint.position, ballista.rotation);

        //    if (newCannon.TryGetComponent<CannonBehaviour>(out CannonBehaviour behaviour))
        //    {
        //        behaviour.ForceDirection(ballista.transform.up);
        //    }
        //}
    }


    private void NewFire()
    {

                //pressedTime += Time.deltaTime;
        if (!shot)
        {



            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                pressedTime += Time.deltaTime;
            }
           
            else
                pressedTime -= Time.deltaTime;

            if (pressedTime > timerRequired)
            {
                Fire();
                shot = true;
            }
            //shot = true;

        }
        else
        {
            pressedTime = 0.0f;
            shot = false;
        }


        pressedTime = Mathf.Clamp(pressedTime, 0.0f, timerRequired);
    }


    private void BallistaVisuals()
    {
        float ratio = (pressedTime / timerRequired) * 100.0f;
        ballistaMesh.SetBlendShapeWeight(0, ratio);
    }


    private void CheckCursorBehaviour()
    {
        if(lockMouse)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

}
