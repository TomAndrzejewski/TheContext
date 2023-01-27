using UnityEngine;

public enum CharacterType { Wizard, Worker };

public class PlayerController : MonoBehaviour
{
    public CharacterType characterType;
    public float speed;
    public bool canMove;
    private Rigidbody2D rb;
    private Vector2 direction;
    private  ParticleSystem particle;
    private CircleCollider2D attackCollider;
    public bool IsAttacking => particle.isPlaying;

  // Start is called before the first frame update
  void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        particle = GetComponent<ParticleSystem>();
        attackCollider = GetComponent<CircleCollider2D>();
  }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        var directionX = Input.GetAxisRaw("Horizontal");
        var directionY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(directionX, directionY);
        if (characterType == CharacterType.Worker && !particle.isPlaying) Rotate();
        if (Input.GetKeyDown(KeyCode.Space)) Attack();
  }

	void FixedUpdate()
	{
        if (!canMove) return;
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed); 
	}

  void Rotate()
  {
    Vector3 mousePosition = Input.mousePosition;
    mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

    Vector3 direction = new Vector3(
        mousePosition.x - transform.position.x,
        mousePosition.y - transform.position.y,
        0.0f
    );

    transform.right = direction;
  }

  void Attack()
	{
    if(!IsAttacking)
      particle.Play();
	}
  private void OnTriggerStay2D(Collider2D collision)
  {
    var enemy = collision.gameObject.GetComponent<Enemy>();
    if (IsAttacking &&  enemy)
      enemy.Die();
  }
}
