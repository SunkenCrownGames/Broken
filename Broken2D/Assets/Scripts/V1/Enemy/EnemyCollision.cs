namespace V1
{
    using UnityEngine;
    
    public class EnemyCollision : MonoBehaviour
    {
        private EnemyHealth m_health;
        private EnemyMovementController m_emv;

        private static GameManager m_gm;

        private void Awake() 
        {
            BindObjects();   
        }

        private void OnTriggerEnter2D(Collider2D p_other)
        {
            BulletCollision(p_other);
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
            m_emv = GetComponent<EnemyMovementController>();

            if(m_gm == null)
                m_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        private void BulletCollision(Collider2D p_other)
        {
            if(p_other.CompareTag("Bullet"))
            {
                m_health.m_health -= p_other.gameObject.GetComponent<ProjectileController>().BulletData.BulletDamage;
                Destroy(p_other.gameObject);
            }
        }
    }
}