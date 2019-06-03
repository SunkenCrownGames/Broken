namespace V1
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
        protected float m_bulletDirection;
        

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

        public float Direction
        {
            get { return m_bulletDirection; }
            set { m_bulletDirection = value; }
        }
    }
}