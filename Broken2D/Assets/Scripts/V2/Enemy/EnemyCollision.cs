namespace V2
{
    using UnityEngine;
    
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField]
        private ScreenShakeData m_screenShakeHitData = null;
        [SerializeField]
        private ScreenShakeData m_screenShakeDeathData = null;

        private EnemyHealth m_health;
        private EnemyEntity m_ee;

        private static GameManager m_gm;
        private static ScreenShakeManager m_ssm;

        private void Awake() 
        {
            BindObjects();   
        }

        private void OnTriggerEnter2D(Collider2D p_other)
        {
            BulletTriggerCollision(p_other);
        }

        private void OnCollisionEnter2D(Collision2D p_other) 
        {
            PlayerCollision(p_other);
        }



        private void Update()
        {
            if(m_health.m_health <= 0)
            {
                m_ee.EnemyData.OrbData.SpawnPosition = transform.position;
                m_gm.OrbEvent.Invoke(m_ee.EnemyData.OrbData);
                m_ssm.ShakeEvent.Invoke(m_screenShakeDeathData);
                Destroy(this.gameObject);
            }
        }


        private void BindObjects()
        {
            m_health = GetComponent<EnemyHealth>();
            m_ee = GetComponent<EnemyEntity>();

            GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
            if(m_gm == null)
                m_gm = gameManagerObj.GetComponent<GameManager>();
            if (m_ssm == null)
                m_ssm = gameManagerObj.GetComponent<ScreenShakeManager>();;
            
        }

        private void BulletTriggerCollision(Collider2D p_other)
        {
            if(p_other.CompareTag("PlayerBullet"))
            {
                m_health.m_health -= p_other.gameObject.GetComponent<ProjectileController>().BulletData.BulletDamage;
                m_ssm.ShakeEvent.Invoke(m_screenShakeHitData);
                m_ee.EntityData.ActionState = EntityActionState.HIT;

                Destroy(p_other.gameObject);
                m_ee.EnemyData.PatrolData.CurrentStatus = PatrolStatus.CHASING;
            }
        }
        
        private void PlayerCollision(Collision2D p_other)
        {
            if(m_ee.EnemyData.EnemyType == EnemyType.SEEK)
            {
                if(p_other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Reduce Health");
                    Destroy(this.gameObject);
                }
            }
        }
    }
}