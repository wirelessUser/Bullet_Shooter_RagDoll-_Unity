using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRend;

    public GameObject linePos_1, linePos_2;

   public GameObject bulletPrefab;

    public float  fireBulletSpeed;

    public Transform LeftHandWithGun;

    public float rotationSpeedLimiter;
    public List<Rigidbody2D> bulletBody;
    void Start()
    {
        mainCamera = Camera.main;
        lineRend.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TrackMouseInput();
    }


    public void TrackMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            Target();
        }

        if (Input.GetMouseButtonUp(0))
        {
            Fire();
        }

    }


    public void Target()
    {
        Vector3 AimDirection = mainCamera.ScreenToWorldPoint(Input.mousePosition) - linePos_1.transform.position;

        float angle = Mathf.Atan2(AimDirection.y, AimDirection.x) * Mathf.Rad2Deg;
        LeftHandWithGun.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle* rotationSpeedLimiter*Time.deltaTime));
        
        lineRend.positionCount = 2;
        lineRend.enabled = true;
        lineRend.SetPosition(0, linePos_1.transform.position);
        lineRend.SetPosition(1, linePos_2.transform.position);

       
    }

    public void Fire()
    {
        lineRend.enabled = false;

        GameObject bullet = Instantiate(bulletPrefab, linePos_1.transform.position, linePos_1.transform.rotation);
        Vector3 dirBullet = (linePos_2.transform.position - linePos_1.transform.position).normalized;
        //bulletBody.Add(bullet.GetComponent<Rigidbody2D>());

        // bulletBody[i].AddForce(linePos_1.transform.right * fireBulletSpeed,ForceMode2D.Impulse);
       // bullet.transform.Translate(bullet.transform.right * fireBulletSpeed, Space.World);
     bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.right*fireBulletSpeed,ForceMode2D.Impulse);
        Debug.Log("bullet.transform.forward*fireBulletSpeed==" + bullet.transform.forward * fireBulletSpeed);

        Destroy(bullet, 3);
        
    }
}
