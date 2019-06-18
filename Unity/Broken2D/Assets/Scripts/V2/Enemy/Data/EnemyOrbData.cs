namespace V2
{
    using UnityEngine;
    
    [System.Serializable]
    public class EnemyOrbData
    {

        private Vec3 m_spawnPosition;

        [SerializeField]
        private OrbType m_orbType;

        [SerializeField]
        private float m_orbSeekSpeed;

        [SerializeField]
        private TrackType m_orbTrackType;


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

        public Vec3 SpawnPosition
        {
            get { return m_spawnPosition; }
            set { m_spawnPosition = value; }
        }

        public TrackType OrbTrackType
        {
            get { return m_orbTrackType; }
            set { m_orbTrackType = value; }
        }
    }
}