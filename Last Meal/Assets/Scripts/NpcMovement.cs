using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public Animator animator;
    public float speed = 2f;
    Vector3 target = Vector3.zero;

    public bool villager = true;

    public GameManager gameManager;

    void Start()
    {
        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameOver == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
        if (gameManager.isPartnerActive && tag == "Villager")
        {
            GetComponent<SpriteRenderer>().sprite = gameManager.spritePartner;
        }
        else if (tag == "Villager")
        {
            GetComponent<SpriteRenderer>().sprite = gameManager.spriteDefault;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("VillagerStop", true);
            animator.SetBool("ThiefStop", true);
        }
    }
}
