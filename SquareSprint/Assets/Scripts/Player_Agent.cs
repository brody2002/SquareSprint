using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Player_Agent : Agent
{
    private BoxMover boxMover;
    private Rigidbody2D body;

    public override void Initialize()
    {
        body = GetComponent<Rigidbody2D>();
        boxMover = GetComponent<BoxMover>();

    }

    public override void OnEpisodeBegin()
    {
        //reset the position of the player on the start position relative to the parent position
        transform.localPosition = new Vector3(-5, 1.5f, 0);
        //reset the velocity of the player
        body.velocity = new Vector2(0, 0);
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        //just need y velocity since we are moving horizontally at a constant velocity
        sensor.AddObservation(body.velocity.y);
        sensor.AddObservation(boxMover.isTouchingGround());
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(0.1f);
        if(actions.DiscreteActions[0] == 1){
            boxMover.Jump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if we hit the collider with tag "end", we win
        if (collision.CompareTag("End"))
        {
            AddReward(1f);
        }
        else if (collision != null)
        {
            AddReward(-1f);
        }
        EndEpisode();
    }

    //hueristic to test the agent
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.Space))
        {
            discreteActionsOut[0] = 1;
        }
    }
}
