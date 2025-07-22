using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // this gameobject needs a rb
public class BirdControler : MonoBehaviour
{
    // this will be on the bird and say how the bird should be controller whether that is will player input or with the AI

    [Header("Settings")]
    [SerializeField] private float forceOfJump; // how hard the bird will jump
    [SerializeField] private Color normalBirdColor; // color of bird when they have not lost
    [SerializeField] private Color lostBirdColor; // color of bird when they lost

    private Rigidbody2D rb; // stores the rb
    private SpriteRenderer spriteRenderer; // stores the spriteRender so we can change the color of the bird when they lose

    [Header("Info about loseing")]
    [SerializeField] private bool didLose = false; // holds info about wheater the bird has lost or not
    // these 2 are serializeable so we can see them in the inspector
    [SerializeField] private float timeWhenLost = -1; // hold the time when the bird lost 

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>(); // get the rigidbody at the start of the game
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) { // if the input is the space key
            Jump(); // we jump
        }

        ChagneBirdColor(); // change the bird color if needed
    }

    private void ChagneBirdColor() {
        // this will change the color of the bird if they lost

        if(spriteRenderer == null) return; // if we dont have the spriteRender we dont change the color

        if(didLose) { //  if the bird lost
            spriteRenderer.color = lostBirdColor; // we change the color to the bird losing color
        } else {
            spriteRenderer.color = normalBirdColor; // else it has its normal color
        }
    }

    public void Jump() {
        rb.velocity = new(0,0); // we reset the velocity so the jump is snappyer
        rb.AddForce(new(0,forceOfJump), ForceMode2D.Impulse); // add and upwards force
    }

    void OnTriggerEnter2D(Collider2D collision) // on trigger enter
    {
        if(collision.CompareTag("Pipe") && didLose == false) { // if the collider is a pipe and we haven't lost
            didLose = true; // the bird lost
            timeWhenLost = Time.time; // and the time we lost was the currect time
        }
    }

    public bool DidBirdLose() {
        // returns a bool that is true if the bird has hit a pipe and false if it hasn't
        return didLose;
    }

    public float WhenDidBirdLose() {
        // returns the time when the bird lost. if the bird hasn't lost then it retuns -1

        if(didLose == false) return -1;

        return timeWhenLost;
    }
}