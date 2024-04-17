using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;


public class JumperAgent : Agent
{
    public GameObject obstacle1;
    public GameObject obstacle2;
    public float minObstacleSpeed = 2.0f;
    public float maxObstacleSpeed = 5.0f;
    private float obstacle1Speed;
    private float obstacle2Speed;
    public float jumpForce = 4.0f;

    public float reward = 1.0f;

    private bool isGrounded = true;

    public override void OnEpisodeBegin()
    {
        isGrounded = true;
        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        this.transform.localRotation = Quaternion.identity;

        // Randomize obstacle speeds
        obstacle1Speed = Random.Range(minObstacleSpeed, maxObstacleSpeed);
        obstacle2Speed = Random.Range(minObstacleSpeed, maxObstacleSpeed);

        // Reset obstacle positions
        int spawnLocation = Random.Range(0, 2);
        if (spawnLocation == 0)
        {
            obstacle1.transform.localPosition = new Vector3(10, 0.5f, 0);
            obstacle2.transform.localPosition = new Vector3(0, 0.5f, -10);
        }
        else
        {
            obstacle1.transform.localPosition = new Vector3(-10, 0.5f, 0);
            obstacle2.transform.localPosition = new Vector3(0, 0.5f, 10);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(obstacle1.transform.localPosition);
        sensor.AddObservation(obstacle2.transform.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Vector3 controlSignal = Vector3.zero;
        controlSignal.y = actionBuffers.ContinuousActions[0];
        if (controlSignal.y > 0.0f && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }
        if (this.transform.localPosition.y == 0.5f)
        {
            isGrounded = true;
        }

        // Move obstacles
        if (obstacle1.transform.localPosition.x < 10)
        {
            obstacle1.transform.Translate(Vector3.right * obstacle1Speed * Time.deltaTime);
        }

        if (obstacle2.transform.localPosition.z < 10)
        {
            obstacle2.transform.Translate(Vector3.forward * obstacle2Speed * Time.deltaTime);
        }


        if (this.transform.localPosition.y < 0 ||
            Vector3.Distance(this.transform.localPosition, obstacle1.transform.localPosition) < 1.0f ||
            Vector3.Distance(this.transform.localPosition, obstacle2.transform.localPosition) < 1.0f)
        {
            SetReward(-reward);
            EndEpisode();
        }
        if (obstacle1.transform.localPosition.x >= 10 && obstacle2.transform.localPosition.z >= 10)
        {
            SetReward(reward);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
    }
}