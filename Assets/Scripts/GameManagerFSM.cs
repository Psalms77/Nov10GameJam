using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public abstract class GMBaseState : BaseState
{
    protected GameManager controller;
    protected GMBaseState(GameManager mono)
    {
        controller = mono;
    }

}


public class GameManagerFSM : BaseFSM
{
    private GameManager controller;
    public GameManagerFSM(GameManager mono)
    {
        controller = mono;
        currentState = new MenuState(mono);

    }

    public class MenuState : GMBaseState
    {
        public MenuState(GameManager mono) : base(mono)
        {
            this.controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

        }
        public override void HandleUpdate()
        {

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

    public class PlanetState : GMBaseState
    {
        public PlanetState(GameManager mono) : base(mono)
        {
            this.controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

        }
        public override void HandleUpdate()
        {

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

    public class MineState : GMBaseState
    {
        public MineState(GameManager mono) : base(mono)
        {
            this.controller = mono;
            EnterState();
        }
        public override void EnterState()
        {

        }
        public override void HandleUpdate()
        {

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


