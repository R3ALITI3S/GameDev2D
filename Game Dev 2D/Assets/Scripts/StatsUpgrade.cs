using UnityEngine;
using TMPro;

public class StatsUpgrade : MonoBehaviour
{
    public float upgradeHealthAmount = 1f;
    public float upgradeDamageAmount = 1f;
    public float upgradeDefenseAmount = 1f;
    public float upgradeSpeedAmount = 1f;

    public GameObject skillBar;
    public Transform healthProgressParent;
    public Transform damageProgressParent;
    public Transform defenseProgressParent;
    public Transform speedProgressParent;

    public TMP_Text healthUpgradeText;
    public TMP_Text damageUpgradeText;
    public TMP_Text defenseUpgradeText;
    public TMP_Text speedUpgradeText;

    public void IncreaseHealth()
    {
        if(upgradeHealthAmount >= 10)
        {
            return;
        } else {
            upgradeHealthAmount++;

            Instantiate(skillBar, healthProgressParent);
            healthUpgradeText.SetText("Health: " + upgradeHealthAmount + " / 10");
        }
    }

    public void DecreaseHealth()
    {
        if (upgradeHealthAmount <= 1)
        {
            return;
        }
        else
        {
            upgradeHealthAmount--;


            if (healthProgressParent.childCount > 1)
            {
                Transform lastBar = healthProgressParent.GetChild(healthProgressParent.childCount - 1);
                Destroy(lastBar.gameObject); // Remove the last skill bar
            }

            healthUpgradeText.SetText("Health: " + upgradeHealthAmount + " / 10");
        }
    }

    public void IncreaseDamage()
    {
        if (upgradeDamageAmount >= 10)
        {
            return;
        }
        else
        {
            upgradeDamageAmount++;

            Instantiate(skillBar, damageProgressParent);
            damageUpgradeText.SetText("Damage: " + upgradeDamageAmount + " / 10");
        }
    }

    public void DecreaseDamage()
    {
        if (upgradeDamageAmount <= 1)
        {
            return;
        }
        else
        {
            upgradeDamageAmount--;


            if (damageProgressParent.childCount > 1)
            {
                Transform lastBar = damageProgressParent.GetChild(damageProgressParent.childCount - 1);
                Destroy(lastBar.gameObject); // Remove the last skill bar
            }

            damageUpgradeText.SetText("Damage: " + upgradeDamageAmount + " / 10");
        }
    }

    public void IncreaseDefense()
    {
        if (upgradeDefenseAmount >= 10)
        {
            return;
        }
        else
        {
            upgradeDefenseAmount++;

            Instantiate(skillBar, defenseProgressParent);
            defenseUpgradeText.SetText("Defense: " + upgradeDefenseAmount + " / 10");
        }
    }

    public void DecreaseDefense()
    {
        if (upgradeDefenseAmount <= 1)
        {
            return;
        }
        else
        {
            upgradeDefenseAmount--;


            if (defenseProgressParent.childCount > 1)
            {
                Transform lastBar = defenseProgressParent.GetChild(defenseProgressParent.childCount - 1);
                Destroy(lastBar.gameObject); // Remove the last skill bar
            }

            defenseUpgradeText.SetText("Defense: " + upgradeDefenseAmount + " / 10");
        }
    }

    public void IncreaseSpeed()
    {
        if (upgradeSpeedAmount >= 10)
        {
            return;
        }
        else
        {
            upgradeSpeedAmount++;

            Instantiate(skillBar, speedProgressParent);
            speedUpgradeText.SetText("Speed: " + upgradeSpeedAmount + " / 10");
        }
    }

    public void DecreaseSpeed()
    {
        if (upgradeSpeedAmount <= 1)
        {
            return;
        }
        else
        {
            upgradeSpeedAmount--;


            if (speedProgressParent.childCount > 1)
            {
                Transform lastBar = speedProgressParent.GetChild(speedProgressParent.childCount - 1);
                Destroy(lastBar.gameObject); // Remove the last skill bar
            }

            speedUpgradeText.SetText("Speed: " + upgradeSpeedAmount + " / 10");
        }
    }
}
