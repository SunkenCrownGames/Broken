namespace V1
{
    using UnityEngine;
    
    public class EnemyCollision : MonoBehaviour
    {
        [SerializeField]
        private ScreenShakeData m_screenShakeHitData;
        [SerializeField]
        private ScreenShakeData m_screenShakeDeathData;

        private EnemyHealth m_health;
        private EnemyController m_emv;

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
                m_emv.EnemyInfo.OrbData.SpawnPosition = transform.position;
                m_gm.OrbEvent.Invoke(m_emv.EnemyInfo.OrbData);
                Destroy(this.gameObject);
            }
        }


        private void BindObjects()
        {
            m_health = GetComponent<EnemyHealth>();
            m_emv = GetComponent<EnemyController>();

            GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
            if(m_gm == null)
                m_gm = gameManagerObj.GetComponent<GameManager>();
            if (m_ssm == null)
                m_ssm = gameManagerObj.GetComponent<ScreenShakeManager>();;
        }

        private void BulletTriggerCollision(Collider2D p_other)
        {
            if(p_other.CompareTag("Bullet"))
            {
                m_health.m_health -= p_other.gameObject.GetComponent<ProjectileController>().BulletData.BulletDamage;
                m_ssm.ShakeEvent.Invoke(m_screenShakeHitData);
                Destroy(p_other.gameObject);
            }
        }
        
        private void PlayerCollision(Collision2D p_other)
        {
            if(m_emv.EnemyInfo.EnemyType == EnemyType.SEEK)
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