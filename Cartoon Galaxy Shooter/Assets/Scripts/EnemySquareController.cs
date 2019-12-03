using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySquareController : MonoBehaviour
{

    public GameObject ExplosionGO;

    Vector2 targetPos;
    float speed;
    float life;

    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        life = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        if(transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if((col.tag == "PlayerShipTag") || (col.tag == "PlayerLaserTag"))
        {
            life--;

            if(life <= 0)
            {
                PlayExplosion();

                Destroy(gameObject);
            }
        }
    }

    private void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
