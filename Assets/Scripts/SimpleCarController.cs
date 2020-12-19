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

    public float maxSteerAngle = 30;
    public float motorForce = 3050;

    #endregion


    private bool engineOn = true;
    private bool auto;
    private int vel;

    private bool stop;

    public void Awake(){
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0,-0.3f,0);
    }
    public void GetInput(){
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        stop = Input.GetKey(KeyCode.Space);

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
            if(frontDriverW.rpm < 150){
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
        frontDriverW.brakeTorque = 100 * motorForce;
        frontPassengerW.brakeTorque = 100 * motorForce;
        rearDriverW.brakeTorque = 100 * motorForce;
        rearPassengerW.brakeTorque = 100 * motorForce;
    }

    public void SetEngine(bool state){
        engineOn = state;
        Debug.Log("ENGINE DEATH ACELERATION NOT POSIBLE IN ->" + this.gameObject.name); 
    }

    public void SetTransmission(bool state){
        Debug.Log("TRANSMISSION DEATH ACELERATION NOT POSIBLE IN ->" + this.gameObject.name); 
        Stop();
        if(state)
            motorForce = 3050;
        else
            motorForce = 50;
    }

    private void FixedUpdate(){
        if(engineOn){
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
