namespace V1
{
    using UnityEngine;

    [System.Serializable]
    public class EnemySeekData
    {
        [SerializeField]
        private float m_horizontalSpeed;
        [SerializeField]
        private float m_verticalSpeed;

        private Vector3 m_startPos;

        private Vector3 m_midPoint;

        private Vector3 m_endPoint;

        [SerializeField]
        private EnemySeekMovementType m_seekMovementType = EnemySeekMovementType.STRAIGHT;

        [SerializeField]
        private float m_currentDuration = 0;

        [SerializeField]
        private float m_duration = 3;


        [SerializeField]
        private bool m_sinWaveToggle;

        [SerializeField]
        private bool m_randomizedDataStatus;

        [SerializeField]
        private SeekRandomizerData m_seekRandomizerData;


        public Vector3 StartPos
        {
            get { return m_startPos; }
            set { m_startPos = value; }
        }

        public Vector3 MidPoint
        {
            get { return m_midPoint; }
            set { m_midPoint = value; }
        }

        public Vector3 EndPoint
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

        public SeekRandomizerData RandomizerData
        {
            get { return m_seekRandomizerData; }
            set { m_seekRandomizerData = value; }
        }

        public bool RandomizedStatus
        {
            get { return m_randomizedDataStatus; }
            set { m_randomizedDataStatus = value; }
        }

        public bool SinWaveToggle
        {
            get { return m_sinWaveToggle; }
            set { m_sinWaveToggle = value; }
        }
    }
}