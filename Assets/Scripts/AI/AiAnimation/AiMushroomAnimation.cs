using UnityEngine;

public class AiMushroomAnimation : AiAnimation
{
    private enum AnimMovesParam
    {
        locomotion,
        attack1,
        attack2,
        attack3,
        death,
        gotHit,
        goMonster,
        goStatue,
    }

    private float GetTime(AnimMovesParam anim)
    {
        switch (anim)
        {
            case AnimMovesParam.attack1:
                return 1.0f;
            case AnimMovesParam.attack2:
                return 1.6f;
             case AnimMovesParam.attack3:
                 return 2.7f;
            case AnimMovesParam.death:
                return 1.4f;
            case AnimMovesParam.gotHit:
                return 0.3f;
            case AnimMovesParam.goMonster:
                return 1.2f;
            case AnimMovesParam.goStatue:
                return 3f;
            default:
                return 1f;
        }

    }
    public override float Death()
    {
        m_Animator.SetBool(AnimMovesParam.death.ToString(), true);
        return GetTime(AnimMovesParam.death);
    }
    
    public override void TakenDamage()
    {
        m_Animator.SetTrigger(AnimMovesParam.gotHit.ToString());
    }

    public override float ReactToPlayer()
    {
        m_Animator.SetTrigger(AnimMovesParam.goStatue.ToString());
        return GetTime(AnimMovesParam.goStatue);
    }

    public override void GoInactive()
    {
        m_Animator.SetTrigger(AnimMovesParam.goMonster.ToString());
    }

    public override void Move(bool toMove)
    {
        if (toMove)
            m_Animator.SetFloat("locomotion", 0.80f); //hardcoded
        else
            m_Animator.SetFloat("locomotion", 0);
    }

    public override float RandomizeAttack()
    {
        AnimMovesParam [] temp = {AnimMovesParam.attack1, AnimMovesParam.attack2, AnimMovesParam.attack3};
        AnimMovesParam attack = temp[Random.Range(0, temp.Length)];
        m_Animator.SetTrigger(attack.ToString());
        return GetTime(attack);
    }
}
