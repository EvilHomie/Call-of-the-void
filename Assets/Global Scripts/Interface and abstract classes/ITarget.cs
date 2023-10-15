public interface ITarget
{
    public void GetCurrentParameters(out float hullHP, out float armorHP, out float shieldHP);
    public void GetMaxParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP);
}

