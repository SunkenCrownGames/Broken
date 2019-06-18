namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class EnemySeekData
    {
        [SerializeField]
        private float m_horizontalSpeed;
        [SerializeField]
        private float m_verticalSpeed;

        private Vec3 m_startPos;

        private Vec3 m_midPoint;

        private Vec3 m_endPoint;

        [SerializeField]
        private float m_currentDuration = 0;

        [SerializeField]
        private float m_duration = 3;

         [SerializeField]
        private EnemySeekMovementType m_seekMovementType = EnemySeekMovementType.STRAIGHT;


        [SerializeField]
        private bool m_trigWaveToggle;

        [SerializeField]
        private float m_magnitude;

        [SerializeField]
        private bool m_randomizedDataStatus;

        [SerializeField]
        private EnemySeekRandomizerData m_seekRandomizerData;

        [SerializeField]
        private WaveType m_waveType = WaveType.SIN;


        public Vec3 StartPos
        {
            get { return m_startPos; }
            set { m_startPos = value; }
        }

        public Vec3 MidPoint
        {
            get { return m_midPoint; }
            set { m_midPoint = value; }
        }

        public Vec3 EndPoint
        {
            get { return m_endPoint; }
            set { m_endPoint = value; }
        }

        public EnemySeekMovementType SeekType
        {
            get { return m_seekMovementType; }
            set { m_seekMovementType = value; }
        }

        public float CurrentDuration
        {
            get { return m_currentDuration; }
            set { m_currentDuration = value; }
        }

        public float Duration
        {
            get { return m_duration; }
            set { m_duration = value; }
        }

        public float HorizontalSpeed
        {
            get { return m_horizontalSpeed; }
            set { m_horizontalSpeed = value; }
        }


        public float VerticalSpeed
        {
            get { return m_verticalSpeed; }
            set { m_verticalSpeed = value; }
        }

        public EnemySeekRandomizerData RandomizerData
        {
            get { return m_seekRandomizerData; }
            set { m_seekRandomizerData = value; }
        }

        public bool RandomizedStatus
        {
            get { return m_randomizedDataStatus; }
            set { m_randomizedDataStatus = value; }
        }

        public bool TrigWaveToggle
        {
            get { return m_trigWaveToggle; }
            set { m_trigWaveToggle = value; }
        }

        public WaveType Type
        {
            get {return m_waveType;}
            set {m_waveType = value;}
        }

        public float Magnitude
        {
            get { return m_magnitude; }
            set { m_magnitude = value; }
        }

    }
}