using UnityEngine;

public class Dino : MonoBehaviour
{
    public float walkSpeed = 2;
    public float jumpForce = 5;

    private MoveState moveState = MoveState.Idle;
    private Transform trnsfrm;
    public Rigidbody2D rgdB2D;
    private Animator animatorController;
    private float walkTime = 0, walkCooldown = 0.2f;

    public void MoveRight()
    {
        if (moveState != MoveState.Jump)
        {
            moveState = MoveState.Walk;
           /*  if (_directionState == DirectionState.Left)
             {
                 _transform.localScale = new Vector3(-_transform.localScale.x, _transform.localScale.y, _transform.localScale.z);
                 _directionState = DirectionState.Right;
             }  */
            walkTime = walkCooldown;
            animatorController.Play("Walk");
        }
    }

    public void Jump()
{
    if (moveState != MoveState.Jump)
    {
        rgdB2D.velocity = (Vector3.up * jumpForce * Time.deltaTime);
        moveState = MoveState.Jump;
        animatorController.Play("Jump");
    }
}

private void Idle()
{
    moveState = MoveState.Idle;
    animatorController.Play("Idle");
}

    private void Start()
    {
        trnsfrm = GetComponent<Transform>();
        rgdB2D = GetComponent<Rigidbody2D>();
        animatorController = GetComponent<Animator>();
        //directionState = transform.localScale.x > 0 ? DirectionState.Right : DirectionState.Left;
    }

private void Update()
{
    if (moveState == MoveState.Jump)
    {
        if (rgdB2D.velocity == Vector2.zero)
        {
            Idle();
        }
    }
    else if (moveState == MoveState.Walk)
    {
        rgdB2D.velocity = Vector2.right * walkSpeed * Time.deltaTime; // ((_directionState == DirectionState.Right ? Vector2.right : -Vector2.right)
                                                                      // * WalkSpeed * Time.deltaTime);
        walkTime -= Time.deltaTime;
        if (walkTime <= 0)
        {
            Idle();
        }
    }
}
}

enum MoveState
{
    Idle, Walk, Jump
}

