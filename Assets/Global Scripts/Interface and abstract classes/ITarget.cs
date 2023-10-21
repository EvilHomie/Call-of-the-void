using UniRx;

public interface ITarget
{
    public void GetCurrentParameters(out FloatReactiveProperty HullHPRP, out FloatReactiveProperty ArmorHPRP, out FloatReactiveProperty ShieldHPRP);
    public void GetStaticParameters(out float maxHullHP, out float maxArmorHP, out float maxShieldHP, out string name);
}

