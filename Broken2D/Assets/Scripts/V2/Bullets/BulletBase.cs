namespace V2
{
    using UnityEngine;
    
    [System.Serializable]
    public class BulletBase
    {
        [SerializeField]
        protected float m_bulletSpeed;
        [SerializeField]
        protected float m_bulletLifeTime;

        [SerializeField]
        protected float m_bulletCurrentLife;

        [SerializeField]
        protected float m_bulletLifeOffset = 1;

        [SerializeField]
        protected float m_bulletHDirection;

        [SerializeField]
        protected float m_bulletVDirection;

        [SerializeField]
        protected float m_bulletDamage;
    
        public void SetBulletData(ShootingData p_shootingData)
        {
            m_bulletSpeed = p_shootingData.BulletSpeed;
            m_bulletLifeTime = p_shootingData.LifeTime;
            m_bulletCurrentLife = p_shootingData.CurrentLifeTime;
            m_bulletLifeOffset = p_shootingData.LifeOffset;
            m_bulletDamage = p_shootingData.BulletDamage;
        }
        public float Speed
        {
            get { return m_bulletSpeed; }
            set { m_bulletSpeed = value; }
        }
        

        public float LifeTime
        {
            get { return m_bulletLifeTime; }
            set { m_bulletLifeTime = value; }
        }

        public float CurrentLife
        {
            get { return m_bulletCurrentLife; }
            set { m_bulletCurrentLife = value; }
        }

        public float LifeOffset
        {
            get { return m_bulletLifeOffset; }
            set { m_bulletLifeOffset = value; }
        }

        public float HDirection
        {
            get { return m_bulletHDirection; }
            set { m_bulletHDirection = value; }
        }

        public float VDirection
        {
            get { return m_bulletVDirection; }
            set { m_bulletVDirection = value; }
        }

        public float BulletDamage
        {
            get { return m_bulletDamage; }
            set { m_bulletDamage = value; }
        }
    }
}