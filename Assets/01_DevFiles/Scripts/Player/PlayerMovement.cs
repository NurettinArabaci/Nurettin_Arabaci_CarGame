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


    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    public void Move(RecordData inputs)
    {
        _rb.velocity = transform.forward * moveSpeed;
        transform.rotation *= Quaternion.Euler(Vector3.up * inputs.inputValue * rotateSpeed);
    }

    public void GivenInputs(RecordData inputs)
    {
        horizontalValue = inputs.inputValue;
    }



    public void ResetInputs()
    {
        horizontalValue = 0;
        _rb.velocity = Vector3.zero;
        _rb.transform.position = initialPosition;
        _rb.transform.rotation = initialRotation;
        
    }
}
