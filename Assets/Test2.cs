using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Test2 : MonoBehaviour
{

    CompositeDisposable disposables = new();



    public ReactiveProperty<float> _health = new();

    public ReactiveProperty<float> HullHP = new();


    


    void SetTarget()
    {
        TestParameters parameters = GetComponent<TestParameters>();

        parameters.TestHeal
            .Where(h => h <0)
            .Subscribe(h =>
        {
            ChangeHp(h);

        }).AddTo(disposables);





        parameters.TestArmor.Subscribe(h => ChangeArmor(h)).AddTo(disposables);

        parameters.TestShield.Subscribe(h => ChangeShield(h)).AddTo(disposables);




    }


   


    void ChangeHp(float h)
    {

    }
    void ChangeArmor(float h)
    {

    }

    void ChangeShield(float h)
    {

    }
}
