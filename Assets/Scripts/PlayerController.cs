using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public CharacterController characterController;

    [Header("Disparo")]
    public Transform bulletSpawn;
    public float fireRate = 0.2f;
    private float nextFireTime;
    public ObjectPool bulletPool;

    [Header("Referencias")]
    public Camera mainCamera;
    public GameObject gameOverPanel;
    public Renderer playerRenderer;

    private Vector2 moveInput;
    private Vector2 lookInput;

    private IPlayerState currentState;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Start()
    {
        SwitchState(new PlayerInvulnerableState());
    }

    private void Update()
    {
        currentState?.UpdateState(this);

        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        characterController.Move(move * speed * Time.deltaTime);

        if (mainCamera != null)
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (playerPlane.Raycast(ray, out float hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                Vector3 direction = targetPoint - transform.position;
                direction.y = 0;
                if (direction.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }

    public void SwitchState(IPlayerState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            if (bulletPool != null)
            {
                GameObject bullet = bulletPool.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = bulletSpawn.position;
                    bullet.transform.rotation = bulletSpawn.rotation;
                    bullet.SetActive(true);
                    Rigidbody rb = bullet.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.linearVelocity = bulletSpawn.forward * 25f;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState?.OnTriggerEnter(this, other);
    }

    public void SetInvulnerableVisual(bool active)
    {
        if (playerRenderer != null)
        {
            playerRenderer.material.color = active ? Color.cyan : Color.white;
        }
    }
}