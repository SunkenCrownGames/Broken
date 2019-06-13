namespace V2
{
    using UnityEngine;
    
    public class ProjectileController : MonoBehaviour
    {
        
        [SerializeField]
        private BulletBase m_bulletData;

        private void Awake() 
        {
            
        }
        
        private void Update()
        {
            Move();
            LifeTime();
        }

        private void Move()
        {
            transform.Translate( new Vector3(m_bulletData.HDirection * m_bulletData.Speed * Time.deltaTime, m_bulletData.VDirection * m_bulletData.Speed * Time.deltaTime, 0));
            //transform.Translate(transform.right * m_bulletData.Speed * Time.deltaTime);
        }

        private void LifeTime()
        {
            m_bulletData.CurrentLife += Time.deltaTime * m_bulletData.LifeOffset;

            if(m_bulletData.CurrentLife > m_bulletData.LifeTime)
                Destroy(gameObject);
        }
        

        public BulletBase BulletData
        {
            get { return m_bulletData; }
            set { m_bulletData = value; }
        }
    }
}