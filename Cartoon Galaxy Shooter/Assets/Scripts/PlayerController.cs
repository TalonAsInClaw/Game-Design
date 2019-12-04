using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerLaserGO;
    public GameObject BulletPosition;
    public GameObject ExplosionGO;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet = (GameObject)Instantiate(PlayerLaserGO);
            bullet.transform.position = BulletPosition.transform.position;
        }

        float x = Input.GetAxisRaw("Horizontal"); //the value will be -1, 0 or 1 (for left, no input, and right)
        float y = Input.GetAxisRaw("Vertical"); //the value will be -1, 0, 1 (for down, no input, and up)

        //compute a vector and normalize it
        Vector2 direction = new Vector2(x, y).normalized;

        //function that computes and sets player position
        Move(direction);
    }

    void Move(Vector2 direction)
    {
        //find the screen limits
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); //bottom left
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); //top right

        //give buffers so the animation doesn't go offscreen
        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 pos = transform.position;

        pos += direction * speed * Time.deltaTime;

        //keep player within bounds of camera view
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        //detect collision of the player with another ship
        if((col.tag == "EnemyShipTag") || (col.tag == "EnemyLaserTag"))
        {
            PlayExplosion();

            //Destroy(gameObject); //destroy player
            //SceneManager.LoadScene("MainMenu"); // transition back to main menu
            
        }
    }

    private void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = transform.position;
    }
}
