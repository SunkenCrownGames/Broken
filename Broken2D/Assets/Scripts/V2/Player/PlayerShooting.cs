namespace V2
{
    using UnityEngine;

    public class PlayerShooting : MonoBehaviour
    {
        
        [SerializeField]
        private GameObject m_bullet = null;

        [SerializeField]
        private GameObject m_bulletSpawnObject = null;

        [SerializeField]
        private ShootingMode m_shootingMode = ShootingMode.TRIGGER;

        private float m_spawnPositionOffset;

        private Entity m_entity;
        private PlayerEntity m_pe;

        private void Awake() 
        {
            BindObjects();
        }

        private void Update()
        {
            ShootTrigger();
        }

        private void BindObjects()
        {
            m_spawnPositionOffset = Mathf.Abs(m_bulletSpawnObject.transform.position.x - transform.position.x);
            m_pe = GetComponent<PlayerEntity>();
        }


        private void ShootTrigger()
        {
            if(m_shootingMode == ShootingMode.TRIGGER)
            {
                if(Input.GetButtonDown("Fire1"))
                    Shoot();
            }
            else
            {
                if(Input.GetButton("Fire1"))
                {
                    //Eventually Limit By AttackSpeed
                    Shoot();
                }
            }
        }

        private void Shoot()
        {
            Debug.Log("SHOOTING DIRECTION: " + m_pe.EntityData.HDirection);
            float direction = (m_pe.EntityData.HDirection == HorizontalDirection.RIGHT) ? 1 : -1;
            Vector3 spawnPosition = transform.position + new Vector3((m_spawnPositionOffset * direction), 0 , 0);
            GameObject bullet = Instantiate(m_bullet, spawnPosition, Quaternion.identity);
            bullet.GetComponent<ProjectileController>().BulletData.Direction = direction;
            m_pe.EntityData.GM.CurrencyEvent.Invoke(-m_pe.PlayerData.CostData.AttackCost);
        }
    }
}