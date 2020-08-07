using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State
    {
        Idle,
        RunningToEnemy,
        RunningFromEnemy,
        BeginAttack,
        Attack,
        BeginPunch,
        Punch,
        BeginShoot,
        Shoot,
        BeginDeath,
        Death,
        EndOfTheGame,
    }

    public enum Weapon
    {
        Pistol,
        Bat,
        Punch,
    }

    public Weapon weapon;
    public float runSpeed;
    public float distanceFromEnemy;
    public Transform target;
    State state;
    Animator animator;
    Vector3 originalPosition;
    Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        state = State.Idle;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void SetState(State newState)
    {
        if (state == State.Death)
        {
            return;
        }

        state = newState;
    }

    [ContextMenu("Attack")]
    void AttackEnemy()
    {
        switch (weapon) 
        {
            case Weapon.Punch:
            case Weapon.Bat:
                state = State.RunningToEnemy;

                break;

            case Weapon.Pistol:
                state = State.BeginShoot;

                break;
        }
    }

    bool RunTowards(Vector3 targetPosition, float distanceFromTarget)
    {
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.00001f) 
        {
            transform.position = targetPosition;
            return true;
        }

        Vector3 direction = distance.normalized;
        transform.rotation = Quaternion.LookRotation(direction);

        targetPosition -= direction * distanceFromTarget;
        distance = (targetPosition - transform.position);

        Vector3 step = direction * runSpeed;
        if (step.magnitude < distance.magnitude) 
        {
            transform.position += step;
            return false;
        }

        transform.position = targetPosition;
        return true;
    }

    void FixedUpdate()
    {
        Debug.Log(state.ToString());
        switch (state) 
        {
            case State.Idle:
                animator.SetFloat("Speed", 0.0f);
                transform.rotation = originalRotation;
                break;

            case State.RunningToEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(target.position, distanceFromEnemy)) 
                {
                    switch (weapon) 
                    {
                        case Weapon.Bat:
                            state = State.BeginAttack;
                            break;

                        case Weapon.Punch:
                            state = State.BeginPunch;
                            break;
                    }
                }
                break;

            case State.BeginAttack:
                animator.SetTrigger("MeleeAttack");
                state = State.Attack;
                break;

            case State.Attack:
                break;
            
            case State.BeginPunch:
                animator.SetTrigger("Punch");
                state = State.Punch;
                break;

            case State.Punch:
                break;

            case State.BeginShoot:
                animator.SetTrigger("Shoot");
                state = State.Shoot;
                break;

            case State.Shoot:
                break;

            case State.RunningFromEnemy:
                animator.SetFloat("Speed", runSpeed);
                if (RunTowards(originalPosition, 0.0f))
                    state = State.Idle;
                break;
            
            case State.BeginDeath:
                animator.SetTrigger("Death");
                state = State.Death;
                break;

            case State.Death:
                break;
        }
    }
}
