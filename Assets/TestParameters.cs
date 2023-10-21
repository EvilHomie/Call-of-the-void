using UniRx;
using UnityEngine;

public class TestParameters : MonoBehaviour
{

    //public float HullHP = 500;
    //public float ArmorHP = 400;
    //public float ShieldHP = 300;

    public ReactiveProperty<float> TestHeal = new();
    public ReactiveProperty<float> TestArmor = new();
    public ReactiveProperty<float> TestShield = new();

}
