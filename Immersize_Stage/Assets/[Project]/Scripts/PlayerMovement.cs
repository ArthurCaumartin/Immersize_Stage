using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _movementSpeed);
    }

    public void SetMoveSpeed(float value)
    {
        _movementSpeed = value;
    }
}
