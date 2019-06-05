namespace V1
{
    using UnityEngine;
    

    [System.Serializable]
    public class OrbSpawnData
    {
        [SerializeField]
        private OrbType m_orbType;

        [SerializeField]
        private float m_orbSeekSpeed;

        [SerializeField]
        private TrackType m_orbTrackType;

        public OrbSpawnData(OrbType p_orbType = OrbType.STATIONARY, float p_orbSeekSpeed = 0, TrackType p_trackType = TrackType.NONE)
        {
            m_orbType = p_orbType;
            m_orbSeekSpeed = p_orbSeekSpeed;
            m_orbTrackType = p_trackType;
        }

        public OrbSpawnData(EnemyOrbData p_enemyOrbData)
        {
            m_orbType = p_enemyOrbData.Type;
            m_orbSeekSpeed = p_enemyOrbData.SeekSpeed;
            m_orbTrackType = p_enemyOrbData.OrbTrackType;
        }

        public OrbType Type
        {
            get { return m_orbType; }
            set { m_orbType = value; }
        }

        public float SeekSpeed
        {
            get { return m_orbSeekSpeed; }
            set { m_orbSeekSpeed = value; }
        }

        public TrackType OrbTrackType
        {
            get { return m_orbTrackType; }
            set { m_orbTrackType = value; }
        }
    }
}