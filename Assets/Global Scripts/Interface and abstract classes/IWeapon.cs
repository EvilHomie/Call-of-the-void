public interface IWeapon
{
    public void SetWeaponParameters(float fireRate, float projectileSpeed, float projectileLifeTime, float energyDMG, float kineticDMG, float weaponDistance, float asteroidMultiplier, float enemyMultiplier);
    public void Shoot();
    public void Stop();
}

