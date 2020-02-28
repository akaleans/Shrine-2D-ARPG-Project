using UnityEngine;
using UnityEditor;
using System.Collections;

class AttackState : IEnemyState
{
    private Enemy parent;

    private float extraRange = .05f;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (parent.MyCooldown >= parent.MyAttackSpeed && !parent.IsAttacking)
        {
            parent.MyCooldown = 0;
            parent.StartCoroutine(Attack());
        }
        if(parent.MyTarget != null)
        {
            float distance = Vector2.Distance(parent.MyTarget.position, parent.transform.position);
            if(distance >= (parent.MyAttackRange + extraRange) && !parent.IsAttacking)
            {
                parent.ChangeState(new FollowState());
            }
        }
        else
        {
            parent.ChangeState(new IdleState());
        }
    }

    public IEnumerator Attack()
    {
        parent.IsAttacking = true;
        parent.MyAnimator.SetTrigger("attack");

        yield return new WaitForSeconds(parent.MyAttackSpeed);

        parent.IsAttacking = false;
    }
}
