using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] GameObject weapon1;
    [SerializeField] GameObject weapon2;
    [SerializeField] GameObject weapon3;



    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(true);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            weapon1.SetActive(true);
            weapon2.SetActive(true);
            weapon3.SetActive(true);
        }
    }
}
