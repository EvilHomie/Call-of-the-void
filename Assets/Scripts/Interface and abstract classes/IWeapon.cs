public interface IWeapon
{
    public void SetWeaponParameters(float fireRate, float projectileSpeed, float projectileLifeTime, float energyDMG, float kineticDMG, float weaponDistance);
    public void Shoot();
    public void Stop();
}

