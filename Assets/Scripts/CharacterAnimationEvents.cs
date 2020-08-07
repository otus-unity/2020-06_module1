using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;
    Character target;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
        target = character.target.GetComponent<Character>();
    }

    void AttackEnd()
    {
        target.SetState(Character.State.BeginDeath);
        character.SetState(Character.State.RunningFromEnemy);
    }

    void PunchEnd()
    {
        target.SetState(Character.State.BeginDeath);
        character.SetState(Character.State.RunningFromEnemy);
    }
    
    void ShootEnd()
    {
        target.SetState(Character.State.BeginDeath);
        character.SetState(Character.State.Idle);
    }
}
