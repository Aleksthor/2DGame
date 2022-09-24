using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalBossScript : MonoBehaviour
{
    public enum BossName
    {
        GoblinSmallBoss

    };

    public BossName bossName;






    private void OnDestroy()
    {
        switch (bossName)
        {
            case BossName.GoblinSmallBoss:
                BossManager.instance.goblinSmallBossDead = true;
                break;

            default:
                break;

        }
    }

}
