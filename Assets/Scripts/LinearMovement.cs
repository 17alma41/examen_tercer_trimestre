using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LinearMovement : MonoBehaviour
{
    Rigidbody2D rb;

    private void Update()
    {
        transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
    }

    public void ShotDirection(float speed, Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = direction * speed;    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player1"))
        {
            print("Player 1 dead");
            GameReset();
        }
        else if (collision.gameObject.CompareTag("Player2"))
        {
            print("Player 2 dead");
            GameReset();
        }
    }

    void GameReset()
    {
        SceneManager.LoadScene("SampleScene");

    }
}
