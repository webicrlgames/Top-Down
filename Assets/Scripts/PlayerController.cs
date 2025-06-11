using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class character_controler : MonoBehaviour
{
    public float gravityForce;
    public float verticalVelocity;
    public CharacterController characterController;
    public Vector2 MovementImput; 
    [Range(1f, 10f)]
    public float movementSpeed;
    public Vector2 LookingImput;
    public GameObject BulletPrefab;
    public Transform spawnPoint;
    public bool canShoot;
    public float fireDelay = 1f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        canShoot = true;
    }
    void Start()
    {

    }

    void Update()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity = gravityForce * Time.deltaTime;
        }
        float MovimientoX = (MovementImput.x * movementSpeed * Time.deltaTime);
        float MovimientoZ = (MovementImput.y * movementSpeed * Time.deltaTime);

        Vector3 movement = new Vector3(MovimientoX, verticalVelocity, MovimientoZ);
        characterController.Move(movement);

        Vector3 look = new Vector3(LookingImput.x, 0f, LookingImput.y);
        transform.LookAt(transform.position + look);
    }
    public void Onlook(InputAction.CallbackContext context)
    {
        LookingImput = context.ReadValue<Vector2>();


    }
    public void OnMove(InputAction.CallbackContext context)
    {
        MovementImput = context.ReadValue<Vector2>();

        Debug.Log("Move_Imput" + MovementImput);

    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (canShoot && context.performed)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        GameObject bulletClone = Instantiate(BulletPrefab, spawnPoint.position, spawnPoint.rotation);
        canShoot = false;

        yield return new WaitForSeconds(fireDelay); // ⏳ Espera

        canShoot = true;
    }
}
