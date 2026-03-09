using UnityEngine;

public class RestaurantSpawn : MonoBehaviour
{
    public GameManager gameManager;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Villager"))
        {
            gameManager.score += 5;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Thief"))
        {
            gameManager.score -= 3;
            Destroy(collision.gameObject);
        }
    }
}
