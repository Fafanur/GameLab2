using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAssist : MonoBehaviour {

    public void EndAttackAnimation()
    {
        if (!Input.GetButton("Fire2"))
        {
            PlayerController.playerController.combatStates = PlayerController.CombatStates.Idle;
        }
    }
}
