using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Properties
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0f;

    [Header("Component Reference")]
    [SerializeField] private CharacterController cc = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private PlayerAnimationEventsHandler playerAnimationEventsHandler = null;

    [Header("WoodenCart Attributes")]
    [SerializeField] private Rigidbody woodenCartRB = null;
    [SerializeField] private ObjectRotator frontWheelsRotator = null;
    [SerializeField] private ObjectRotator backWheelsRotator = null;
    [SerializeField] private List<LogStackHandler> logStackHandlers = new List<LogStackHandler>();

    [Header("Particle Effects")]
    [SerializeField] private GameObject logBurst = null;

    private VariableJoystick movementJoystick = null;
    private int logStackIndex = 0;
    #endregion

    #region Delegates
    public delegate void PlayerMechanism();
    private PlayerMechanism playerMechanism;
    #endregion

    #region MonoBehaviour Functions
    private void Start()
    {
        movementJoystick = FindObjectOfType<VariableJoystick>();

        EnableMovementFunction(true);
    }

    private void FixedUpdate()
    {
        if (playerMechanism != null)
        {
            playerMechanism();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Tree")
        {
            ActiveTree = other.gameObject.transform;
            
            if (playerMechanism != null)
            {
                EnableMovementFunction(false);
            }
            animator.SetTrigger("CutTree");

            transform.rotation = Quaternion.LookRotation((other.gameObject.transform.position - transform.position).normalized);
            playerAnimationEventsHandler.TreeToDestroy = other.gameObject.transform;

            EnableWoodenCartMovement(false);
        }
    }
    #endregion

    #region Getter And Setter
    public bool WoodenCartFull { get; set; }

    public Transform ActiveTree { get; private set; }
    #endregion

    #region Public Functions
    public void EnableMovementFunction(bool value)
    {
        if (value)
        {
            playerMechanism += Movement;
        }
        else
        {
            playerMechanism -= Movement;
        }
    }
    #endregion

    #region Core Functions
    private void Movement()
    {
        Vector3 direction = new Vector3(movementJoystick.Horizontal, 0, movementJoystick.Vertical).normalized;
        cc.Move(direction * Time.deltaTime * moveSpeed);

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
            animator.SetBool("Run", true);

            EnableWoodenCartMovement(true);
        }
        else
        {
            animator.SetBool("Run", false);

            EnableWoodenCartMovement(false);
        }
    }

    private void EnableWoodenCartMovement(bool value)
    {
        if (value)
        {
            woodenCartRB.isKinematic = false;
            frontWheelsRotator.EnableRotation(true);
            backWheelsRotator.EnableRotation(true);
        }
        else
        {
            woodenCartRB.isKinematic = true;
            frontWheelsRotator.EnableRotation(false);
            backWheelsRotator.EnableRotation(false);
        }
    }
    #endregion

    #region Public Functions
    public void FillWoodenLog()
    {
        Destroy(Instantiate(logBurst, ActiveTree.position, Quaternion.identity), 5f);

        if (!WoodenCartFull)
        {
            if (logStackHandlers[logStackIndex].IsStackFull())
            {
                logStackIndex++;

                if (logStackIndex >= logStackHandlers.Count)
                {
                    WoodenCartFull = true;
                }
            }
            else
            {
                logStackHandlers[logStackIndex].FillLog();
            }
        }
    }
    #endregion
}
