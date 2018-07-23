using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour {
    [SerializeField]
    private Camera cameraPlayer;

    [SerializeField]
    private GameObject cam;


    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float camerarotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;

    private Rigidbody rb;


	// Ao iniciar
	void Start ()
    {
        rb = GetComponent<Rigidbody>();    

	}
	
	//  Verifica todo frame
	void Update () {
	
	}

    //Vetor de movimento
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }


    //Vetor de rotação
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    
    //Vetor de rotação camera
    public void RotateCamera(float _camerarotation)
    {
        camerarotationX = _camerarotation;
    }

    //Pegando vetor para propulsão
    public void ApplyThruster(Vector3 _thrusterforce)
    {
        thrusterForce = _thrusterforce;
    }


    //Toda vez que tem uma iteração física
    //pra melhorar a rotação e movimento
    void FixedUpdate()
    {
        PerformeMovement();
        PerformeRotation();
    }

    void PerformeMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce != Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void PerformeRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(cam != null)
        {
            //cam.transform.Rotate(-camerarotation);
            
            //configurando a nova rotação e travando 
            //nova rotação da camera
            currentCameraRotationX -= camerarotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
            //aplicando a rotação na camera 
            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }
}
