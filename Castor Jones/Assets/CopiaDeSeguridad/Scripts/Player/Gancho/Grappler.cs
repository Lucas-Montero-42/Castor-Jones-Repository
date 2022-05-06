using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappler : MonoBehaviour
{
    
    
    
    [Header("Player:")]
    [SerializeField] private GameObject player;
    private Rigidbody2D playerRigidbody;
    private bool moveSlowPlayer;
    public bool _moveSlowPlayer => moveSlowPlayer;

    private bool isGrappling;
    private bool playerCanMove = true;
    public float m_DeathAimController = 0.19f;
    public GameObject pivotMirilla;
    public GameObject Mirilla;
    public ControllerType controller;
    public bool _playerCanMove => playerCanMove;


    [Header("Scripts Ref:")]
    public GrapplerRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] private int InteractuableLayer = 10;
    [SerializeField] private int Movil = 11;


    [Header("Main Camera:")]
    public Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    [Header("Objeto Mov:")]
    [SerializeField] public float velocityObjetoMov;
    private float velocityObjMovX;
    private Rigidbody2D rigidbodyObjMov;
    private bool isObjectMov;
    private Vector2 velocityObjMov;
    private GroundChecker groundChekerObjectMov;

    [Header("Scope:")]
    [SerializeField] private GameObject grappleScope;
    [SerializeField] private LayerMask allMasks;
    Vector2 targetScope;




    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;
    GameObject target;

    [Header("Otras variables")]

    [SerializeField] private float timeMargeGancho;
    private float timerMargeGancho = 0;

    private bool moveYes;

    private bool activeButton = false;
    private buttonManager Button;
    [SerializeField] private float timeButton;
    private float timerBotton = 0;


    /// <summary>
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        playerRigidbody = player.GetComponent<Rigidbody2D>();

    }

    [Obsolete]
    private void Update()
    {
        if (controller == ControllerType.TECLADO)
        {
            pivotMirilla.SetActive(false);
        }
        else if (controller == ControllerType.MANDO)
        {
            pivotMirilla.SetActive(true);
        }

        if (controller == ControllerType.TECLADO)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                SetGrapplePoint();
                timerMargeGancho = 0;
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                Gancho();
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                desactivateGrappler();
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }
        else if (controller == ControllerType.MANDO)
        {
            if (Input.GetButtonDown("FireGrappler"))
            {
                SetGrapplePoint();
                timerMargeGancho = 0;
            }
            else if (Input.GetButton("FireGrappler"))
            {
                Gancho();
            }
            else if (Input.GetButtonUp("FireGrappler"))
            {
                desactivateGrappler();
            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }

        if (isObjectMov)
            ObjectMovUpdate();
        if (controller == ControllerType.TECLADO)
        {
            ScopeUpdate();
        }
        else if (controller == ControllerType.MANDO)
        {
            ScopeUpdateController();
        }

        if (activeButton)
            ButtonUpdate();

    }

    private void Gancho()
    {
        if (grappleRope.enabled)
        {
            isGrappling = true;
            RotateGun(grapplePoint, false);
        }
        else
        {
            timerMargeGancho += Time.deltaTime;
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            RotateGun(mousePos, true);
            if (timerMargeGancho <= timeMargeGancho)
                SetGrapplePoint();
        }

        if (launchToPoint && grappleRope.isGrappling)
        {
            if (launchType == LaunchType.Transform_Launch)
            {
                Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                Vector2 targetPos = grapplePoint - firePointDistnace;
                gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
            }
        }
    }

    private void ButtonUpdate()
    {
        Button = target.transform.gameObject.GetComponent<buttonManager>();
        timerBotton += Time.deltaTime;
        if (timerBotton >= timeButton)
        {
            activeButton = false;
            Button.Activado = true;
            timerBotton = 0;
        }
        if (Button.Activado)
        {
            StartCoroutine(ButtonUpdateTimer());
        }
            
        else
        {
            grapplePoint = target.transform.position;
        }

    }

    private void ObjectMovUpdate()
    {

        groundChekerObjectMov = target.GetComponent<GroundChecker>();
        grapplePoint = target.transform.position;
        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;

        velocityObjMovX = 0;

        if (playerRigidbody.velocity.x > 1 && grappleDistanceVector.x <= 0)
        {
            velocityObjMovX += velocityObjetoMov;
        }
        else if (playerRigidbody.velocity.x < -1 && grappleDistanceVector.x >= 0)
        {
            velocityObjMovX += -velocityObjetoMov;
        }

        
        if (grappleDistanceVector.x <= 0)
        {
            velocityObjMovX += velocityObjetoMov/2;
        }
        else
        {
            velocityObjMovX += -velocityObjetoMov/2;
        }
        

        if (groundChekerObjectMov.IsGrounded == true)
        { 
            rigidbodyObjMov = target.GetComponent<Rigidbody2D>();
            velocityObjMov = new Vector2(velocityObjMovX, 0);
            rigidbodyObjMov.velocity = velocityObjMov;
        }

    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        if (controller == ControllerType.TECLADO)
        {
            Vector3 distanceVector = lookPoint - gunPivot.position;

            float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
            if (rotateOverTime && allowRotationOverTime)
            {
                gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
            }
            else
            {
                gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else if (controller == ControllerType.MANDO)
        {
            RotateGunController();
        }
        
    }
    void RotateGunController()
    {
        float l_AimHorizontal = Input.GetAxisRaw("Aim Horizontal");
        float l_AimVertical = Input.GetAxisRaw("Aim Vertical");

        if (Mathf.Abs(l_AimHorizontal) > m_DeathAimController || Mathf.Abs(l_AimVertical) > m_DeathAimController)
        {
            float l_Angle = Mathf.Atan2(-l_AimHorizontal, -l_AimVertical);
            l_Angle = Mathf.Rad2Deg * (l_Angle + Mathf.PI * 0.5f);
            pivotMirilla.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, l_Angle);
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(l_Angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }

    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = new Vector2(0,0);
        if (controller == ControllerType.TECLADO)
        {
            distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        }
        else if (controller == ControllerType.MANDO)
        {
            distanceVector = Mirilla.transform.position - gunPivot.position;
        }

        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            target = _hit.transform.gameObject;

            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                    moveYes = true;
                    playerCanMove = false;
                }
            }
            else if (_hit.transform.gameObject.layer == InteractuableLayer)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
                if (_hit.transform.gameObject.tag == "Button")
                {
                    grapplePoint = target.transform.position;
                    activeButton = true;
                    moveYes = true;
                    playerCanMove = false;
                }
            }
            else if (_hit.transform.gameObject.layer == Movil)
            {
                if (_hit.transform.gameObject.tag == "Carretilla")
                {                   
                    grapplePoint = _hit.point;                    
                    grappleRope.enabled = true;
                    moveYes = false;
                    moveSlowPlayer = true;
                    isObjectMov = true;
                }
            }
        }
    }

    public void Grapple()
    {
        if (moveYes)
        {
            m_springJoint2D.autoConfigureDistance = false;
            if (!launchToPoint && !autoConfigureDistance)
            {
                m_springJoint2D.distance = targetDistance;
                m_springJoint2D.frequency = targetFrequncy;
            }
            if (!launchToPoint)
            {
                if (autoConfigureDistance)
                {
                    m_springJoint2D.autoConfigureDistance = true;
                    m_springJoint2D.frequency = 0;
                }

                m_springJoint2D.connectedAnchor = grapplePoint;
                m_springJoint2D.enabled = true;
            }
            else
            {
                switch (launchType)
                {
                    case LaunchType.Physics_Launch:
                        m_springJoint2D.connectedAnchor = grapplePoint;

                        Vector2 distanceVector = firePoint.position - gunHolder.position;

                        m_springJoint2D.distance = distanceVector.magnitude;
                        m_springJoint2D.frequency = launchSpeed;
                        m_springJoint2D.enabled = true;
                        break;
                    case LaunchType.Transform_Launch:
                        m_rigidbody.gravityScale = 0;
                        m_rigidbody.velocity = Vector2.zero;
                        break;
                }
            }
        }
    }

    public void desactivateGrappler()
    {
        isGrappling = false;
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rigidbody.gravityScale = 1;
        timerMargeGancho = timeMargeGancho + 10;
        moveSlowPlayer = false;
        isObjectMov = false;
        playerCanMove = true;
        activeButton = false;
    }

    

    [Obsolete]
    private void ScopeUpdate()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5000, allMasks);


        if (hit.collider != null)
        {
            targetScope = hit.point;
        }

        if (!isGrappling)
        {
            grappleScope.transform.position = targetScope;
            grappleScope.gameObject.active = true;
        }
        else
            grappleScope.gameObject.active = false;
    }
    private void ScopeUpdateController()
    {
        Vector2 direction = Mirilla.transform.position - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 5000, allMasks);


        if (hit.collider != null)
        {
            targetScope = hit.point;
        }

        if (!isGrappling)
        {
            grappleScope.transform.position = targetScope;
            grappleScope.gameObject.active = true;
        }
        else
            grappleScope.gameObject.active = false;
    }

    private IEnumerator ButtonUpdateTimer()
    {
        yield return new WaitForSeconds(0.3f);
        grapplePoint = target.transform.position + new Vector3(0, -0.5f, 0);
    }

}
