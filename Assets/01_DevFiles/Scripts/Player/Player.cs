using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Matchable, IAttackable
{
    #region Enums
    public enum State
    {
        Recording,
        Playback,
        Reset
    }
    public State currentState;
    #endregion

    #region Private Variables
    private ActionRecorder _recorder;
    private PlayerMovement _movement;
    private SpriteRenderer _activateSign;
    private Rigidbody _rb;

    private bool newPlayback = false;
    private float timer;
    private float playbackTimer;
    #endregion

    private void Awake()
    {
        Initialize();
    }

    //caches components and set some fields
    private void Initialize()
    {
        _recorder = GetComponent<ActionRecorder>();
        _movement = GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
        _activateSign = GetComponentInChildren<SpriteRenderer>();

        currentState = State.Reset;
        timer = 0;
        playbackTimer = 0;
    }


    void FixedUpdate()
    {
        if (GameManager.Instance.gameState != GameState.Play) return;

        if (currentState == State.Recording)
        {

            timer += Time.deltaTime;
            _recorder.Recording(timer);
            _movement.Move(_recorder.GetInputData());

        }

        if (currentState == State.Playback)
        {

            if (newPlayback)
            {
                playbackTimer = 0;
                newPlayback = false;
            }

            playbackTimer += Time.deltaTime;


            if (_recorder.KeyExists(playbackTimer))
            {
                RecordData recordedInputs = _recorder.GetRecordedInputs(playbackTimer);

                _movement.Move(recordedInputs);

            }

        }
    }

    public void Recording()
    {
        timer = 0;
        _recorder.ClearHistory();
        currentState = State.Recording;
        
    }


    public void Playback()
    {
        newPlayback = true;
        currentState = State.Playback;
    }

    public void ResetState()
    {
        _movement.ResetInputs();
        currentState = State.Reset;
        _recorder.ResetInput();
        activate = ActiveState.Passive;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Gate gate))
        {
            if (gate.activate != activate) return;

            if (PlayerManager.Instance.LastPlayer() == this)
            {
                GameStateEvent.Fire_OnChangeGameState(GameState.Win);
            }

            if (gate.activate == ActiveState.Active)
                PlayerManager.Instance.NextPlayer();

            gate.EnterGate();

            

            _movement.StopMove();

        }

        if (other.TryGetComponent(out IAttackable attackable))
        {
            if (attackable.Attack(ActiveState.Passive)) return;

            PlayerManager.Instance.ResetPlayer();
        }
    }

    public override void ChangeState(ActiveState _state)
    {
        base.ChangeState(_state);

        _activateSign.enabled = activate == ActiveState.Active;

    }

    public bool Attack(ActiveState state)
    {
        return state == activate;
    }
}
