using Cinemachine.Examples;
using UnityEngine;

public class DeadlyItemFP : MonoBehaviour
{
    GameObject  player;
    GameObject gameManager;

    private void Start()
    {
        player      = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("Game Manager");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetType() == typeof(CapsuleCollider))
        {
            PlayerDies(collision.gameObject.tag.ToString());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(CapsuleCollider))
        {
            PlayerDies(other.tag.ToString());
        }
    }

    void PlayerDies(string chico)
    {
        if (chico.Equals("Player"))
        {
            if (player.GetComponent<BoxDetector>().isHolding)
            {
                player.GetComponent<BoxDetector>().DropBox();
            }

            gameManager.GetComponent<GameManagerFirstPerson>().Invoke("Died", 0);
        }
    }
}
