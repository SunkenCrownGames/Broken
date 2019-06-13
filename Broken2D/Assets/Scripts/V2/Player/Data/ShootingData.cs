namespace V2
{
    using UnityEngine;
    
    [System.Serializable]
    public class ShootingData
    {
        [SerializeField]
        private int m_bulletCount = 1;
        [SerializeField]
        private float m_bulletDamage = 150;
        [SerializeField]
        private float m_bulletSpeed = 5;
        [SerializeField]
        private float m_currentLifeTime = 0;
        [SerializeField]
        private float m_lifeTime = 5;
        [SerializeField]
        private float m_lifeOffset = 1;

        [SerializeField]
        private float m_angleOffset;

        public int BulletCount
        {
            get { return m_bulletCount; }
            set { m_bulletCount = value; }
        }

        public float BulletDamage
        {
            get { return m_bulletDamage; }
            set { m_bulletDamage = value; }
        }

        public float BulletSpeed
        {
            get { return m_bulletSpeed; }
            set { m_bulletSpeed = value; }
        }

        public float CurrentLifeTime
        {
            get { return m_currentLifeTime; }
            set { m_currentLifeTime = value; }
        }

        public float LifeTime
        {
            get { return m_lifeTime; }
            set { m_lifeTime = value; }
        }

        public float LifeOffset
        {
            get { return m_lifeOffset; }
            set { m_lifeOffset = value; }
        }

        public float AngleOffset
        {
            get { return m_angleOffset; }
            set { m_angleOffset = value; }
        }
    }
}