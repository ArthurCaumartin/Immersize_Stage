using UnityEngine;

public class AttackOverLap : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _damage = 10;
    [SerializeField] private float _raduis = 2;
    void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _raduis, _layerMask);
        foreach (var item in cols)
        {
            Character c = item.GetComponent<Character>();
            if (!c) continue;
            c.TakeDamage(_damage * Time.deltaTime);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .2f);
        Gizmos.DrawSphere(transform.position, _raduis);
    }
}