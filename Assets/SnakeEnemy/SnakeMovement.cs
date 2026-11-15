using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SnakeMovement : MonoBehaviour
{
    private Transform target;

    private Vector3 offsetPointToSwimTo;

    [SerializeField] private float offsetForFindingPlaceAroundPlayer;
    [SerializeField] private float swimSpeed;
    [SerializeField] private Collider2D theCollider;
    [SerializeField] private GameObject particleSystemSand;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireForce;
    [SerializeField] private SpriteRenderer spriteRendererer;

    [SerializeField] private Animator animatorController;
    
    public bool isMoving = true;
    public bool isBurroued;

    private float stateTimer;
    
    public void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if (isMoving)
        {
            if (!isBurroued)
            {
                return;
            }

            spriteRendererer.enabled = false;
            if (Vector3.Distance(target.position, target.position + offsetPointToSwimTo) < 0.2f)
            {
                //we are close to our target.
                ChangeStateToFire();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position + offsetPointToSwimTo, swimSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (isBurroued)
            {
            }
            else
            {
                ShootPrefab();
                ChangeStateToMove();
            }
        }
    }

    private void ChangeStateToMove()
    {
        Debug.Log("Change state to move");
        theCollider.enabled = false;
        stateTimer = 0;
        isMoving = true;
        particleSystemSand.SetActive(true);
        
        //play borrough animation
        animatorController.Play("Borough");

        offsetPointToSwimTo = new Vector3(Random.Range(-offsetForFindingPlaceAroundPlayer, offsetForFindingPlaceAroundPlayer), Random.Range(-offsetForFindingPlaceAroundPlayer, offsetForFindingPlaceAroundPlayer), 0);
    }

    private void ChangeStateToFire()
    {
        isMoving = false;
        stateTimer = 0f;
        particleSystemSand.SetActive(false);
        spriteRendererer.enabled = true;
        
        Debug.Log("Change state to fire");
        //play unborrough animation
        animatorController.Play("Borough");
    }

    public void ShootPrefab()
    {
        GameObject spawnObj = Instantiate(bulletPrefab, transform.position, transform.rotation);
        if (spawnObj != null)
        {
            Rigidbody2D bulletRigidBody = spawnObj.GetComponent<Rigidbody2D>();
            bulletRigidBody.AddForce(transform.up * fireForce);
        }
    }
    
    
    private void SetIsBorughedToTrue()
    {
        SetIsBorughed(true);
    }
    private void SetIsBorughedToFalse()
    {
        SetIsBorughed(false);
    }
    private void SetIsBorughed(bool what)
    {
        isBurroued = what;
        if (isBurroued) spriteRendererer.enabled = false;
    }
}
