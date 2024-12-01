using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    Animator animator;
    Movement movementScript;
    SpriteRenderer spriteRenderer;
    ReplayPlayerMovement ghost;

    private bool jumpStarted = false;

    private void Start()
    {
        if (!TryGetComponent<Animator>(out animator))
            Debug.Log("Animator not set!", this);

        if (!TryGetComponent<Movement>(out movementScript))
            Debug.Log("Movement script not set!", this);

        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
            Debug.Log("Sprite renderer not set!", this);

        if (!TryGetComponent<ReplayPlayerMovement>(out ghost))
            Debug.Log("Sprite renderer not set!", this);
    }

    private void Update()
    {
        if(ghost.killTimer > 1.9f)
            PlayAnim("Death");

        if (ghost.killTimer <= 0)
        {
            switch (movementScript.HorizontalInputData)
            {
                case 1:
                    spriteRenderer.flipX = false;
                    break;
                case -1:
                    spriteRenderer.flipX = true;
                    break;
            }

            if (movementScript.GetVelocityY() > 0.01 && !jumpStarted)
            {
                PlayAnim("JumpStart");
                jumpStarted = true;
            }
            else if (movementScript.GetVelocityY() < -0.01)
            {
                PlayAnim("JumpDown");
                jumpStarted = false;
            }
            else if (movementScript.OnGround)
            {
                switch (movementScript.HorizontalInputData)
                {
                    case 0:
                        PlayAnim("Idle");
                        break;
                    case 1:
                        PlayAnim("Run");
                        spriteRenderer.flipX = false;
                        break;
                    case -1:
                        PlayAnim("Run");
                        spriteRenderer.flipX = true;
                        break;
                }
            }
        }
    }


    private void PlayAnim(string animName)
    {
        animator.Play(animName);
    }
}
