using UnityEngine;

//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]

public class PlayerControler : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;

    private bool isLocalPlayer;
    /*
    #region Fuel and Thruster

    //MEXER DEPOIS
    [SerializeField]
    private float thrusterForce = 1000f;

    [SerializeField]
    private float thrusterFuelBurnSpeed = 1f;

    [SerializeField]
    private float thrusterFuelRegenSpeed = 0.3f;
    private float thrusterFuelAmount = 1f;

    public float GetThrusterFuelAmount()
    {
        return thrusterFuelAmount;
    }

    #endregion
    */
    [SerializeField]
    private LayerMask enviromentMask;


    [Header("Spring settings:")]


    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;


    //component caching
    private PlayerMotor motor;
   // private ConfigurableJoint joint;
    //private Animator animator;

    void Start()
    {
        motor = GetComponent<PlayerMotor>();
        //LockCursor();
        //MEXER DEPOIS
        //joint = GetComponent<ConfigurableJoint>();
        //SetJointSettings(jointSpring);

       // animator = GetComponent<Animator>();
    }

        void LockCursor()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Update()
    {
        /*if (PauseMenu.IsOn)
            return;*/
        //Verificando se estamos em alguma superficie que não seja o chão 
       /* RaycastHit _hitGround;
        if(Physics.Raycast (transform.position, Vector3.down, out _hitGround, 100f, enviromentMask))
        {
            joint.targetPosition = new Vector3(0f, -_hitGround.point.y, 0f);
        }
        else
        {
            joint.targetPosition = new Vector3(0f, 0f, 0f);
        }*/



        //provisório para travar o cursor
       if(Input.GetKey(KeyCode.L))
        {
            LockCursor();
        }


        //To Calculate movement velocity as a 3D Vector (Calcular a velocidade de movimento como um vetor 3D)
        if (isLocalPlayer)
        {
            float _xMov = Input.GetAxis("Horizontal");
            float _zMov = Input.GetAxis("Vertical");

            // Calculate rotation as a 3d vector 
            float _yRot = Input.GetAxisRaw("Mouse X");
            // Calculate rotation as a 3d vector 
            float _xRot = Input.GetAxisRaw("Mouse Y");

            Vector3 _movHorizontal = transform.right * _xMov;
            Vector3 _movVertical = transform.forward * _zMov;

            //final movement vector
            Vector3 _velocity = (_movHorizontal + _movVertical) * speed;


            //movimento de animação
            // animator.SetFloat("ForwardVelocity", _zMov);

            //Aplicando movimento

            motor.Move(_velocity);



            Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

            // Aplicando a rotação

            motor.Rotate(_rotation);



            float _camerarotationX = _xRot * lookSensitivity;
            //new Vector3(_xRot, 0f, 0f) * lookSensitivity;

            // Aplicando a rotação na camera

            motor.RotateCamera(_camerarotationX);

        }
        //calculando pulo (propulsão)
 /*       Vector3 _thrusterForce = Vector3.zero;

        if (Input.GetButton("Jump")&& thrusterFuelAmount > 0f)
        {   
            thrusterFuelAmount -= thrusterFuelBurnSpeed * Time.deltaTime;

            if (thrusterFuelAmount >= 0.01f)
            {
                _thrusterForce = Vector3.up * thrusterForce;
                SetJointSettings(0f);
            }
        }
        else
        {
            thrusterFuelAmount += thrusterFuelRegenSpeed * Time.deltaTime;

            SetJointSettings(jointSpring);
        }
        

        //Limitando os valores do combustivel entre 0 e 1 
        thrusterFuelAmount = Mathf.Clamp(thrusterFuelAmount, 0f, 1f);

        //MEXER DEPOIS
        //aplicando a propulsão

        motor.ApplyThruster(_thrusterForce);
        



        
    */
    }


    public void ProcessInputs()
    {
        isLocalPlayer = true;
    }
    public void DisableInputs()
    {
        isLocalPlayer = false;
    }
    /*
    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive {
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }*/
}
