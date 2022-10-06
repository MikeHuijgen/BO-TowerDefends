using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    [SerializeField]private float dartSpeed;
    private Transform _target;
    private Transform _tower;
    private int _damage;

    public void SetUpDart(Transform target,Transform tower,int towerDamage)
    {
        _target = target;
        _tower = tower;
        _damage = towerDamage;
        ResetPos();
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        GoToTarget();
    }

    private void GoToTarget()
    {
        if (_target == null)
        {
            transform.position = _tower.position;
            gameObject.SetActive(false);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position,_target.position, dartSpeed * Time.deltaTime);
        transform.LookAt(_target);
        if (Vector3.Distance(transform.position, _target.position) <= 0.001f)
        {
            DamageTarget();
            gameObject.SetActive(false);
            ResetPos();
        }
    }

    private void ResetPos()
    {
        transform.position = _tower.position;
    }

    private void DamageTarget()
    {
        _target.GetComponent<Enemy>().DecreaseHealth(_damage, _tower);
        gameObject.SetActive(false);
    }
}
