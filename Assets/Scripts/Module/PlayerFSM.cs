using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class PlayerBaseState : BaseState
{
    protected PlayerController controller;
    protected PlayerBaseState(PlayerController mono)
    {
        controller = mono;
    }

}


public class PlayerFSM : BaseFSM
{
    private PlayerController controller;
    public PlayerFSM(PlayerController mono)
    {
        controller = mono;
        currentState = new IdleState(mono);

    }

    public class IdleState : PlayerBaseState
    {
        public IdleState(PlayerController mono) : base(mono)
        {
            this.controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

        }
        public override void HandleUpdate()
        {
            controller.MoveOnPlanet();
            controller.JumpOnPlanet();
        }
        public override void HandleFixedUpdate()
        {

        }
        public override void ExitState()
        {

        }
        public override void HandleCollide2D(Collision2D collision)
        {

        }

        public override void HandleTrigger2D(Collider2D collider)
        {

        }
    }

}


