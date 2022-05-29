using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ADSRManager : MonoBehaviour
{
    [SerializeField] private float initialForce = 1f;

    [SerializeField] private float attackDuration = 2f;
    [SerializeField] private AnimationCurve attack;

    [SerializeField] private float decayDuration = 0.25f;
    [SerializeField] private AnimationCurve decay;

    [SerializeField] private float sustainDuration = 3f;
    [SerializeField] private AnimationCurve sustain;

    [SerializeField] private float releaseDuration = 2f;
    [SerializeField] private AnimationCurve release;

    // timeBeforeRestart determines how much time should pass
    // before the next ADSR phase occurs.
    [SerializeField] private float timeBeforeRestart = 3f;

    // isMovingLeft can be toggled to decide the initial direction of movement.
    [SerializeField] private bool isMovingLeft = true;

    private float timeElapsed;

    private float attackTimer;
    private float decayTimer;
    private float sustainTimer;
    private float releaseTimer;

    private float _finalForce;
    public float FinalForce
    {
        get => _finalForce;
        set => _finalForce = value;
    }

    private enum Phase { Attack, Decay, Sustain, Release, None};

    private Phase currentPhase;

    // At Start, immediately begin with an Attack Phase.
    void Start()
    {
        this.FinalForce = 0f;
        this.ResetTimers();
        this.currentPhase = Phase.Attack;
        this.timeElapsed = 0f;
    }

    void Update()
    {
        // While Phase isn't None, update the finalForce with the corresponding ADSR curves.
        if (this.currentPhase != Phase.None)
        {
            this.FinalForce = this.initialForce * this.ADSREnvelope();
            if (this.isMovingLeft)
            {
                this.FinalForce *= -1f;
            }
        }
        // Otherwise, count time until timeBeforeRestart time has passed.
        // Then, begin another Attack Phase.
        else 
        {
            this.timeElapsed += Time.deltaTime;
            // Once enough time has elapsed, start again by moving to Attack Phase.
            if (this.timeElapsed > this.timeBeforeRestart)
            {
                // Flip the direction of the wind.
                this.isMovingLeft = !this.isMovingLeft;
                this.timeElapsed = 0f;
                this.ResetTimers();
                this.currentPhase = Phase.Attack;
            }
        }
    }

    float ADSREnvelope()
    {
        float velocity = 0.0f;

        if (Phase.Attack == this.currentPhase)
        {
            velocity = this.attack.Evaluate(this.attackTimer / this.attackDuration);
            this.attackTimer += Time.deltaTime;
            if(this.attackTimer > this.attackDuration)
            {
                this.currentPhase = Phase.Decay;
            }
        } 
        else if (Phase.Decay == this.currentPhase)
        {
            velocity = this.decay.Evaluate(this.decayTimer / this.decayDuration);
            this.decayTimer += Time.deltaTime;
            if (this.decayTimer > this.decayDuration)
            {
                this.currentPhase = Phase.Sustain;
            }
        } 
        else if (Phase.Sustain == this.currentPhase)
        {
            velocity = this.sustain.Evaluate(this.sustainTimer / this.sustainDuration);
            this.sustainTimer += Time.deltaTime;
            if (this.sustainTimer > this.sustainDuration)
            {
                this.currentPhase = Phase.Release;
            }
        } 
        else if (Phase.Release == this.currentPhase)
        {
            velocity = this.release.Evaluate(this.releaseTimer / this.releaseDuration);
            this.releaseTimer += Time.deltaTime;
            if (this.releaseTimer > this.releaseDuration)
            {
                this.currentPhase = Phase.None;
            }
        }
        return velocity;
    }

    private void ResetTimers()
    {
        this.attackTimer = 0.0f;
        this.decayTimer = 0.0f;
        this.sustainTimer = 0.0f;
        this.releaseTimer = 0.0f;
    }
}
