using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool canMove;
    private Rigidbody2D rb;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        var directionX = Input.GetAxisRaw("Horizontal");
        var directionY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(directionX, directionY);
    }

	void FixedUpdate()
	{
        if (!canMove) return;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
	}
}
