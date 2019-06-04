namespace V1
{
    using UnityEngine;
    
    public class EnemyCollision : MonoBehaviour
    {
        private EnemyHealth m_health;

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
                Destroy(this.gameObject);
            }
        }


        private void BindObjects()
        {
            m_health = GetComponent<EnemyHealth>();
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