using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SimpleCarController : NetworkBehaviour
{

    #region WheelController
    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;

    public WheelCollider frontDriverW, frontPassengerW;
    public WheelCollider rearDriverW, rearPassengerW;
    public Transform frontDriverT,frontPassengerT;
    public Transform rearDriverT, rearPassengerT;

    [SerializeField]private float maxSteerAngle = 30;
    [SerializeField]private float motorForce;

    private float motorForceBuffer;

    #endregion

    
    

    #region  DamageBehaviorVariables
    private bool engineOn = true;
    private bool driverOn = true;
    private bool radiatorOn = true;

    #endregion
    private Rigidbody body;
    private bool auto;
    

    public void Awake(){
        body = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-0.3f,0);
        motorForceBuffer = motorForce;
        maxVel = maxVelBuffer;
    }
    public void GetInput(){
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        if(m_verticalInput != 0){
            auto = false;
            maxVel = maxVelBuffer;
        }         
            

        if(Input.GetKeyDown(KeyCode.Space)){
            stop = ! stop;
        }        
        if(Input.GetKeyDown(KeyCode.E)){
            NextVelControl(true);
            NextVel();
            auto = true;
        }            
        if(Input.GetKeyDown(KeyCode.Q)){
            NextVelControl(false);
            NextVel();
            auto = true;
        }
        if(auto){
            m_verticalInput = vel;
        }
    }
    #region Automatic Velocity Control
    
    private int vel;
    private int maxVelBuffer = 75;
    private int maxVel;
    private int velControl = 0;
    float speed;
    private bool stop;

    public void NextVelControl(bool dir){
        if(dir){
            if(velControl < 4){
                velControl++;
            }
        }else{
            if(velControl > -2){
                velControl--;
            }
        }
    }

    public void NextVel(){
        switch (velControl)
        {
            case 1:
            Debug.Log("PRIMERA");
            maxVel = 25;
            vel = 1;
            break;
            case 2:
            Debug.Log("SEGUNDA");
            maxVel = 40;
            vel = 1;
            break;
            case 3:
            Debug.Log("TERCERA");
            maxVel = 75;
            vel = 1;
            break;
            case -1:
            Debug.Log("REVERSA");
            maxVel = 15;
            vel = -1;
            break;            
            default:
            Debug.Log("NEUTRO");
            maxVel = 0;
            break;
        }
    }

    #endregion

    #region WheelColliderBehavior
    private void Steer(){
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }
    private void Accelerate(){
        float value = m_verticalInput * motorForce;
        speed = body.velocity.sqrMagnitude;

        if(stop){
            SetTorque(0,motorForce,0,motorForce,0,motorForce,0,motorForce);
        }        
        else{
            if(speed < maxVel){
                SetTorque(value,0,value,0,value,0,value,0);
            }else{
                SetTorque(0,0,0,0,0,0,0,0);
            }         
        }
    }

    public void SetTorque(float frontDWA, float frontDWB,float frontPWA, float frontPWB,float rearDWA,float rearDWB,float rearPWA, float rearPWB){
        frontDriverW.motorTorque = frontDWA;
        frontDriverW.brakeTorque = frontDWB;
        frontPassengerW.motorTorque = frontPWA;
        frontPassengerW.brakeTorque = frontPWB;
        rearDriverW.motorTorque = rearDWA;
        rearDriverW.brakeTorque = rearDWB;
        rearPassengerW.motorTorque = rearPWA;
        rearPassengerW.brakeTorque = rearPWB;
    }

    private void UpdateWheelPoses(){
        UpdateWheelPose(frontDriverW,frontDriverT);
        UpdateWheelPose(frontPassengerW,frontPassengerT);
        UpdateWheelPose(rearDriverW,rearDriverT);
        UpdateWheelPose(rearPassengerW,rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider,Transform _transform){
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);
        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void Stop(){
        frontDriverW.brakeTorque = 100 * motorForceBuffer;
        frontPassengerW.brakeTorque = 100 * motorForceBuffer;
        rearDriverW.brakeTorque = 100 * motorForceBuffer;
        rearPassengerW.brakeTorque = 100 * motorForceBuffer;
    }

    #endregion

    #region SetPartBehavior
        
   
    public void SetEngine(bool state){
        engineOn = state;
        //Debug.Log("ENGINE DEATH ACELERATION NOT POSIBLE IN ->" + this.gameObject.name); 
    }

    public void SetDriver(bool state){
        driverOn = state;
        //Debug.Log("DRIVER DEATH DIRECTION CONTROL NOT POSIBLE IN ->" + this.gameObject.name); 
    }

    public void SetRadiator(bool state){
        if(state)
            motorForce = motorForceBuffer;
        else
            motorForce = 560;        
    }

    public void SetTransmission(bool state){
        //Debug.Log("TRANSMISSION DEATH ACELERATION NOT POSIBLE IN ->" + this.gameObject.name); 
        Stop();
        if(state)
            motorForce = motorForceBuffer;
        else
            motorForce = 50;
    }

     #endregion

    void Update(){
        if(!isLocalPlayer)
            return;
        if(driverOn)
            GetInput();
    }

    private void FixedUpdate(){
        if(!isLocalPlayer)
            return;
        
        if(engineOn){
            Accelerate();
            Steer();
            UpdateWheelPoses();
        }            
        else{
            Stop();
        }        
    }
}
