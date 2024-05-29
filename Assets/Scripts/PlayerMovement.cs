using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] KeyCode upKey = KeyCode.W;
    [SerializeField] KeyCode downKey = KeyCode.S;
    [SerializeField] KeyCode leftKey = KeyCode.A;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    [SerializeField] float gridSize = 1f;
    [SerializeField] float normalMoveTime = 0.3f;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] float sprintDecreaseRate = 0.1f; 
    [SerializeField] KeyCode shootKey = KeyCode.Space;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletSpeed = 5f;

    [SerializeField] Canvas gameOverUI;

    Vector2 currentPosition;
    Vector2 targetPosition;
    Vector2 movement;
    Vector3 lookDirection = Vector2.right;

    bool isMoving = false;
    float moveTime;

    void Start()
    {
        moveTime = normalMoveTime;
    }

    void Update()
    {
        HandleMovementInput();
        HandleShootingInput();
    }

    void HandleMovementInput()
    {
        if (Input.GetKeyDown(upKey))
        {
            movement.y += 1;
            lookDirection = Vector2.up;
            RotatePlayer(0f);
        }
        else if (Input.GetKeyDown(downKey))
        {
            movement.y -= 1;
            lookDirection = Vector2.down;
            RotatePlayer(180f);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            movement.x += 1;
            lookDirection = Vector2.right;
            RotatePlayer(270f);
        }
        else if (Input.GetKeyDown(leftKey))
        {
            movement.x -= 1;
            lookDirection = Vector2.left;
            RotatePlayer(90f);
        }

        if ((movement != Vector2.zero) && !isMoving)
        {
            if (Input.GetKey(sprintKey))
            {
                moveTime = sprintDecreaseRate;
            }
            else
            {
                moveTime = normalMoveTime;
            }
            StartCoroutine(MovePlayer());
        }
    }

    void HandleShootingInput()
    {
        if (Input.GetKeyDown(shootKey))
        {
            ShootProjectile();
        }
    }

    IEnumerator MovePlayer()
    {
        isMoving = true;

        currentPosition = transform.position;
        targetPosition = currentPosition + movement * gridSize;

        yield return new WaitForSeconds(moveTime);

        transform.position = targetPosition;
        movement = Vector2.zero;
        isMoving = false;
    }

    void ShootProjectile()
    {
        GameObject bullet = Instantiate(
            bulletPrefab, 
            transform.position + lookDirection,
            Quaternion.identity);

        LinearMovement linearMovement = bullet.GetComponent<LinearMovement>();
        linearMovement.ShotDirection(bulletSpeed, lookDirection);

        Destroy(bullet, 2f);
    }

    void RotatePlayer(float angle)
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
