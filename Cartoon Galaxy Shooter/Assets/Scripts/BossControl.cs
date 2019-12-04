using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    float speed;
    int life;

    // Start is called before the first frame update
    void Start()
    {
        speed = 4f;
        life = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //get position of enemy
        Vector2 position = transform.position;

        //get position of camera
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //calculate new position vector
        if (position.x < min.x + .5f || position.x > Camera.main.orthographicSize * Screen.width / Screen.height - .5f)
        {
            ChangeDirection();
        }
        position = new Vector2(position.x - speed * Time.deltaTime, position.y);

        //move enemy to new position
        transform.position = position;

        //if beyond camera view, destroy
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "PlayerShipTag") || (col.tag == "PlayerLaserTag"))
        {
            PlayExplosion();
            if (life > 1) life--;
            else Destroy(gameObject);
        }
    }

    private void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }

    private void ChangeDirection()
    {
        speed *= -1;
    }
}
