using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationEvents : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    void AttackEnd()
    {
        character.target.GetComponentInParent<Character>().SetState(Character.State.Death);
        character.SetState(Character.State.RunningFromEnemy);
    }

    void ShootEnd()
    {
        character.target.GetComponentInParent<Character>().SetState(Character.State.Death);
        character.SetState(Character.State.Idle);
    }
}
