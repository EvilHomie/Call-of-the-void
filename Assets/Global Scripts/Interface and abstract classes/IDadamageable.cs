public interface IDadamageable
{
    public void Damage(float energyDMG, float kineticDMG, float asteroidMultiplier, float enemyMultiplier);

    public void SetMaxHpParameters(float hullHP, float armorHP, float shieldHP);

    public void SetRegRates(float hullRegRate, float armorRegRate, float shieldRegRate, float shieldStartRegDelay);
}