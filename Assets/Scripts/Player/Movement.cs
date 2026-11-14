using UnityEngine;
using UnityEngine.InputSystem;

public class Movement: MonoBehaviour {
    public float speed = 0.5f;
    public SpriteRenderer spRenderer;
    public Sprite faceDown;
    public Sprite faceUp;
    public Sprite faceLeft;
    public Sprite faceRight;
    [SerializeField] private InputActionAsset actions;

    private Rigidbody2D rb;
    private Vector2 input;
    private InputAction move;
    public bool canMove = true;

    private void Awake()
    {
        move = actions.FindAction("Move");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    // Update is called once per frame
    void Update() {
        input = move.ReadValue<Vector2>();

        if (input.x != 0 || input.y != 0) {
            // Update sprite based on input direction
            if (input.x > 0) {
                spRenderer.sprite = faceRight;
            } else if (input.x < 0) {
                spRenderer.sprite = faceLeft;
            } else if (input.y > 0) {
                spRenderer.sprite = faceUp;
            } else if (input.y < 0) {
                spRenderer.sprite = faceDown;
            }
        } else {
            spRenderer.sprite = faceDown;
        }

        input.Normalize();
    }

    private void FixedUpdate() {
        if (!canMove) {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = input * speed;
    }
}