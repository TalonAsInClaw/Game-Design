using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGun : MonoBehaviour
{
    public GameObject BossLaserGO;
    bool enemyFires = false;

    // Update is called once per frame
    void Update()
    {
        if (enemyFires == false)
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        float randomFire = Random.Range(0f, 1f);
        enemyFires = true;
        yield return new WaitForSeconds(randomFire);
        Invoke("FireEnemyLaser", 0f);
        enemyFires = false;
    }

    void FireEnemyLaser()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");

        if (playerShip != null)
        {
            GameObject laser = (GameObject)Instantiate(BossLaserGO);

            laser.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - laser.transform.position;

            laser.GetComponent<BossLaser>().SetDirection(direction);
        }
    }
}
