using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCarController : MonoBehaviour
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


    private bool engineOn = true;
    private bool driverOn = true;
    private bool radiatorOn = true;
    private bool auto;
    private int vel;

    private bool stop;

    public void Awake(){
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-0.3f,0);
        motorForceBuffer = motorForce;
    }
    public void GetInput(){
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space)){
            stop = ! stop;
        }

        if(Input.GetAxis("Vertical") != 0){            
            auto = false;
        }        
        else if(Input.GetKeyDown(KeyCode.E)){
            auto = !auto;
            vel = 1;
        }            
        else if(Input.GetKeyDown(KeyCode.Q)){
            auto = !auto;
            vel = -1;
        }

        if(auto){
            m_verticalInput = vel;
        }
    }

    private void Steer(){
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }
    private bool reverse; bool foward = false;
    private void Accelerate(){
        float value = m_verticalInput * motorForce;
        if(stop){
            SetTorque(0,motorForce,0,motorForce,0,motorForce,0,motorForce);
        }else{
            if(frontDriverW.rpm < 50){
                SetTorque(value,0,value,0,value,0,value,0);
            }            
        }
    }

    public void SetTorque(float frontDWA, float frontDB,float frontPWA, float frontPWB,float rearDWA,float rearDWB,float rearPWA, float rearPWB){
        frontDriverW.motorTorque = frontDWA;
        frontDriverW.brakeTorque = frontDB;
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

    public void SetEngine(bool state){
        engineOn = state;
        //Debug.Log("ENGINE DEATH ACELERATION NOT POSIBLE IN ->" + this.gameObject.name); 
    }

    public void SetDriver(bool state){
        driverOn = state;
        //Debug.Log("DRIVER DEATH DIRECTION CONTROL NOT POSIBLE IN ->" + this.gameObject.name); 
    }

    public void SetRadiator(bool state){
        
        if(state){
            motorForce = motorForceBuffer;
        }else{
            motorForce = 560;
        }
    }

    public void SetTransmission(bool state){
        //Debug.Log("TRANSMISSION DEATH ACELERATION NOT POSIBLE IN ->" + this.gameObject.name); 
        Stop();
        if(state)
            motorForce = motorForceBuffer;
        else
            motorForce = 50;
    }

    private void Update(){
        if(engineOn){
            if(driverOn)
                GetInput();
            Accelerate();
            Steer();
            UpdateWheelPoses();
        }            
        else{
            Stop();
        }        
    }
    
    


}
