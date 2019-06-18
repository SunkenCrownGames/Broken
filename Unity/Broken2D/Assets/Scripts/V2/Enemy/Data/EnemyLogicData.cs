namespace V2
{
    using UnityEngine;

    public class EnemyLogicData
    {
        private static GameObject m_player;

        private int m_playerLayer;

        
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