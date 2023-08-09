using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float horizontalValue;

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float rotateSpeed = 3f;

    private Rigidbody _rb;


    private void Awake()
    {
        Initialize();
    }

    #region Subscribe and UnSubscribe events
    private void OnEnable()
    {
        GameStateEvent.OnWinGame += StopMove;
    }
    private void OnDisable()
    {
        GameStateEvent.OnWinGame -= StopMove;
    }
    #endregion

    private void Initialize()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    public void Move(RecordData inputs)
    {
        _rb.velocity = transform.forward * moveSpeed * Time.deltaTime*100;
        transform.rotation *= Quaternion.Euler(Vector3.up * inputs.inputValue * rotateSpeed * Time.deltaTime*100);
    }

    public void StopMove()
    {
        _rb.velocity = Vector3.zero;
    }

    public void GivenInputs(RecordData inputs)
    {
        horizontalValue = inputs.inputValue;
    }



    public void ResetInputs()
    {
        horizontalValue = 0;
        StopMove();
        _rb.transform.position = initialPosition;
        _rb.transform.rotation = initialRotation;
        
    }
}
