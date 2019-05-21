// @author Olli Paakkunainen

using System.Collections;
using UnityEngine;

public class pendulum : MonoBehaviour
{
    private HingeJoint2D hinge;
    private Direction moveDirection = Direction.Left;
    

    private void Awake()
    {
        hinge = GetComponent<HingeJoint2D>();
    }

    private void Update()
    {
        switch (moveDirection)
        {
            case Direction.Left:
                if(hinge.limitState == JointLimitState2D.UpperLimit)
                {
                    JointMotor2D newMotor = new JointMotor2D();
                    newMotor.maxMotorTorque = hinge.motor.maxMotorTorque;
                    newMotor.motorSpeed = -hinge.motor.motorSpeed;
                    hinge.motor = newMotor;
                    hinge.useMotor = true;
                    moveDirection = Direction.Right;
                }
                break;
            case Direction.Right:
                if (hinge.limitState == JointLimitState2D.LowerLimit)
                {
                    JointMotor2D newMotor = new JointMotor2D();
                    newMotor.maxMotorTorque = hinge.motor.maxMotorTorque;
                    newMotor.motorSpeed = -hinge.motor.motorSpeed;
                    hinge.motor = newMotor;
                    hinge.useMotor = true;
                    moveDirection = Direction.Left;
                }
                break;
        }
    }
}