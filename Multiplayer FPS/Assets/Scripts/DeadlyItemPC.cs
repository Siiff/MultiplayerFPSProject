using Cinemachine.Examples;
using UnityEngine;

public class DeadlyItemPC : MonoBehaviour
{
    GameObject  player;
    GameManager gameManager;
    Animator    animPlayer;

    private void Start()
    {
        player      = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        animPlayer  = player.GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerDies(collision.gameObject.tag.ToString());
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerDies(other.tag.ToString());
    }

    void PlayerDies(string chico)
    {
        if (chico.Equals("Player"))
        {
            if (animPlayer.GetBool("CarryingBox"))
            {
                player.GetComponent<DropItemPC>().DropBox();
            }

            player.GetComponent<DropItemPC>().AnimNotBox();
            player.GetComponent<Player>().enabled = false;
            animPlayer.SetTrigger("Die");
            gameManager.Invoke("Died", 3);
        }
    }
}
