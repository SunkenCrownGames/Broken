namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class EnemyData
    {
        [SerializeField]
        private EnemyType m_enemyType;

        [SerializeField]
        private EnemyMovementData m_movementData;

        [SerializeField]
        private Bounds m_bounds;

        [SerializeField]
        private EnemySeekData m_enemySeekData;

        [SerializeField]
        private EnemyPatrolData m_enemyPatrolData;

        [SerializeField]
        private EnemyOrbData m_orbData;


        private static GameObject m_player;

        private int m_playerLayer;

        public EnemyType EnemyType
        {
            get { return m_enemyType; }
            set { m_enemyType = value; }
        }

        public EnemyMovementData MovementData
        {
            get { return m_movementData; }
            set { m_movementData = value; }
        }

        public Bounds Bounds
        {
            get { return m_bounds; }
            set { m_bounds = value; }
        }

        public EnemySeekData SeekData
        {
            get { return m_enemySeekData; }
            set { m_enemySeekData = value; }
        }

        public EnemyOrbData OrbData
        {
            get { return m_orbData; }
            set {m_orbData = value; }
        }

        public EnemyPatrolData PatrolData
        {
            get { return m_enemyPatrolData; }
            set {m_enemyPatrolData = value; }
        }

        public int PlayerLayer
        {
            get { return m_playerLayer; }
            set { m_playerLayer = value; }
        }

        public GameObject Player
        {
            get { return m_player; }
            set { m_player = value; }
        }

    }
}