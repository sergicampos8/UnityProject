using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float verticalForce = 250f;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSR;
    [SerializeField] private ParticleSystem playerParticles;
    [SerializeField] private Color orangeColor;
    [SerializeField] private Color cyanColor;
    [SerializeField] private Color violetColor;
    [SerializeField] private Color pinkColor;
    [SerializeField] private float restartDelay = 1f;
    private string currentColor;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerSR = GetComponent<SpriteRenderer>();
        playerSR.color = Color.red;
        ChangeColor();
    }

    void Update()
    {
        // Detectar toque en la pantalla
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(new Vector2(0, verticalForce));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ColorChanger"))
        {
            ChangeColor();
            Destroy(collision.gameObject);
            return;
        }
        if (!collision.gameObject.CompareTag(currentColor))
        {
            GameManager.LastScore = (int)gameObject.transform.position.y;
            gameObject.SetActive(false);
            Instantiate(playerParticles, transform.position, Quaternion.identity);

            Invoke("ReturnToMainMenu", restartDelay);
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void ChangeColor()
    {
        int randomNumber = Random.Range(0, 4);
        switch (randomNumber)
        {
            case 0:
                playerSR.color = orangeColor;
                currentColor = "Orange";
                break;
            case 1:
                playerSR.color = cyanColor;
                currentColor = "Cyan";
                break;
            case 2:
                playerSR.color = violetColor;
                currentColor = "Violet";
                break;
            case 3:
                playerSR.color = pinkColor;
                currentColor = "Pink";
                break;
        }
    }
}

public static class GameManager
{
    public static int LastScore { get; set; }
}
