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
            Debug.Log("Shooting Direction: " + direction);
            Vector3 spawnPosition = transform.position + new Vector3((m_spawnPositionOffset * direction), 0 , 0);

            if(m_pe.PlayerData.ShootingData.BulletCount == 1)
                SingleBullet(direction, spawnPosition);
            else
                MultipleBulletFire(direction, spawnPosition);
        }

        private void SingleBullet(float p_direction, Vector3 p_spawnPosition)
        {
            SpawnBullet(p_spawnPosition, p_direction);     
        }

        private void MultipleBulletFire(float p_direction, Vector3 p_spawnPosition)
        {
            float bulletCount = m_pe.PlayerData.ShootingData.BulletCount;

            bool oddNumber = ((bulletCount % 2) == 0) ? false : true;
            int bulletCounter = 0;
            float angleOffset = m_pe.PlayerData.ShootingData.AngleOffset;

            if(oddNumber)
            {
                for(int i = 0; i < bulletCount; i++)
                {
                    if(i == 0)
                    {
                        SpawnBullet(p_spawnPosition, p_direction);
                    }
                    else
                    {

                        if(bulletCounter == 2)
                        {
                            angleOffset += m_pe.PlayerData.ShootingData.AngleOffset;
                            bulletCounter = 0;
                        }
                        else
                        {
                            bulletCounter++;
                        }

                        if((i % 2) != 0)
                        {
                            float xDirection = Mathf.Cos(Mathf.Deg2Rad * angleOffset) * p_direction;
                            float yDirection = Mathf.Sin(Mathf.Deg2Rad * angleOffset) * p_direction;
                            SpawnBullet(p_spawnPosition, xDirection, yDirection, angleOffset);
                        }
                        else
                        {
                            float xDirection = Mathf.Cos(-Mathf.Deg2Rad * angleOffset) * p_direction;
                            float yDirection = Mathf.Sin(-Mathf.Deg2Rad * angleOffset) * p_direction;
                            SpawnBullet(p_spawnPosition, xDirection, yDirection, -angleOffset);
                        }
                    }
                    
                }
            }
            else
            {
                for(int i = 0; i < bulletCount; i++)
                {
                    if(bulletCounter == 2)
                    {
                        angleOffset += m_pe.PlayerData.ShootingData.AngleOffset;
                         bulletCounter = 0;
                    }
                    else
                    {
                        bulletCounter++;
                    }
                    if((i % 2) != 0)
                    {
                        float xDirection = Mathf.Cos(Mathf.Deg2Rad * angleOffset) * p_direction;
                        float yDirection = Mathf.Sin(Mathf.Deg2Rad * angleOffset) * p_direction;
                        SpawnBullet(p_spawnPosition, xDirection, yDirection, angleOffset);
                    }
                    else
                    {
                        float xDirection = Mathf.Cos(-Mathf.Deg2Rad * angleOffset) * p_direction;
                        float yDirection = Mathf.Sin(-Mathf.Deg2Rad * angleOffset) * p_direction;
                        SpawnBullet(p_spawnPosition, xDirection, yDirection, -angleOffset);
                    }  
                }
            }
        }

        private void SpawnBullet(Vector3 p_spawnPosition, float p_hDirection, float p_vDIrection = 0, float p_angle = 0)
        {
            GameObject bullet = Instantiate(m_bullet, p_spawnPosition, Quaternion.Euler(0,0, p_angle));
            //Debug.Log("Passed Angle: " + p_angle);
            ProjectileController projectileComp = bullet.GetComponent<ProjectileController>();
            projectileComp.BulletData.HDirection = p_hDirection;
            projectileComp.BulletData.VDirection = p_vDIrection;
            projectileComp.BulletData.SetBulletData(m_pe.PlayerData.ShootingData);
            m_pe.LogicData.GM.CurrencyEvent.Invoke(-m_pe.PlayerData.CostData.AttackCost);
        }
    }
}