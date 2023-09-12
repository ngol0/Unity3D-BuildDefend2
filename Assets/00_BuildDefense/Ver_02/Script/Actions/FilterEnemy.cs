using UnityEngine;

public class FilterEnemy : MonoBehaviour
{
    Unit unit;
    [SerializeField] WeaponBase weaponType;

    private void Awake() 
    {
        unit = GetComponentInParent<Unit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            var enemy = other.GetComponent<Health>();
            if (enemy && !enemy.IsDead())
            {
                if (unit) unit.GetAction<FightAction>().StartAction(enemy, weaponType, unit);
            }
        }
    }
}
