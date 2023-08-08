using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject Expprefab;

    public float rangeExplosion;

    Vector2 explosionPos;
    public Collider2D[] colliders;
    private void Awake()
    {
        explosionPos = transform.position ;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.GetComponent<Bullet>())
        {
            GameObject ExoplosionEfefct = Instantiate(Expprefab);
            ExoplosionEfefct.transform.position = transform.position;
            Destroy(gameObject);
            Destroy(ExoplosionEfefct, 9);

            ExplosionCode();

        }

        if (collision.gameObject.tag=="Goons")
        {
            collision.gameObject.tag = "Untagged";
        }
    }


    void ExplosionCode()
    {
         colliders = Physics2D.OverlapCircleAll(explosionPos, rangeExplosion);

        
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Rigidbody2D>() != null)
            {
                collider.GetComponent<Rigidbody2D>().AddForce((collider.transform.position - transform.position) *0.005f,ForceMode2D.Impulse);
                Debug.Log("Addforce Collider Hit==" + (collider.transform.position - transform.position));
            }
        }

    }

    //private void OnDrawGizmosSelected()
    //{

    //    Gizmos.color = Color.gray;
    //    Gizmos.DrawSphere(transform.position, rangeExplosion);
    //}
}
