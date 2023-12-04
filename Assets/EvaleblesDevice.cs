using System.Collections.Generic;
using UnityEngine;

public class EvaleblesDevice : MonoBehaviour
{
    public static List<Cargo> evaleblesEngines = new();
    public static List<Cargo> evaleblesRCSs = new();
    public static List<Cargo> evaleblesShields = new();
    public static List<Cargo> evaleblesGenerators = new();

    public static List<TractorBeam> evaleblesTractorBeams = new();
    [SerializeField] List<TractorBeam> testEvaleblesTractorBeams;

    public static List<Cargo> evaleblesRepairDrones = new();

    public static List<Cargo> evaleblesCargos = new();
    [SerializeField] List<Cargo> testEvaleblesCargos;

    


    private void Awake()
    {
        evaleblesCargos = testEvaleblesCargos;
        evaleblesTractorBeams = testEvaleblesTractorBeams;
    }
}
