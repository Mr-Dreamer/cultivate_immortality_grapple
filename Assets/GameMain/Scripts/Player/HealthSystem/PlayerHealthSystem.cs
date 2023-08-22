// ---------------------------------------------------------------
// 文件名称：PlayerHealthSystem.cs
// 创 建 者：赵志伟
// 创建时间：2023/08/22
// 功能描述：玩家生命系统
// ---------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using Grapple;
using UnityEngine;

public class PlayerHealthSystem : CharacterHealthSystemBase
{
    private bool canExecute = false;

    protected override void Update()
    {
        base.Update();

        OnHitLockTarget();
    }

    public override void TakeDamager(float damagar, string hitAnimationName, Transform attacker)
    {
        SetAttacker(attacker);

        if (CanParry())
        {
            Parry(hitAnimationName);
        }
        else
        {
            m_Animator.Play(hitAnimationName, 0, 0f);
            GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Hit);
        }
    }

    #region Parry

    private bool CanParry()
    {
        if (m_Animator.CheckAnimationTag("Parry")) return true;
        if (m_Animator.CheckAnimationTag("ParryHit")) return true;

        return false;
    }

    private void Parry(string hitName)
    {
        if (!CanParry()) return;

        switch (hitName)
        {
            default:
                m_Animator.Play(hitName, 0, 0f);
                GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Hit);
                break;
            case "Hit_D_Up":
                //_animator.Play("ParryF", 0, 0f);
                //GameAssets.Instance.PlaySoundEffect(_audioSource, SoundAssetsType.parry);

                if (m_CurrentAttacker.TryGetComponent(out CharacterHealthSystemBase health))
                {
                    health.FlickWeapon("Flick_0");
                    GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                }

                canExecute = true;

                //游戏时间缓慢 给玩家处决反应时间
                Time.timeScale = 0.25f;
                GameObjectPoolSystem.Instance.TakeGameObject("Timer").GetComponent<Timer>().CreateTime(0.25f, () =>
                {
                    canExecute = false;

                    if (Time.timeScale < 1f)
                    {
                        Time.timeScale = 1f;
                    }
                }, false);
                break;
            case "Hit_H_Right":
                m_Animator.Play("ParryL", 0, 0f);
                GameAssetsManager.Instance.PlaySound(m_AudioSource, SoundAssetsType.Parry);
                break;
        }
    }

    #endregion

    #region Hit

    private bool CanHitLockAttacker()
    {
        return true;
    }

    private void OnHitLockTarget()
    {
        //检测当前动画是否处于受伤状态
        if (m_Animator.CheckAnimationTag("Hit") || m_Animator.CheckAnimationTag("ParryHit"))
        {
            transform.rotation = transform.LockOnTarget(m_CurrentAttacker, transform, 50f);
        }
    }

    #endregion

    public bool GetCanExecute() => canExecute;
}
