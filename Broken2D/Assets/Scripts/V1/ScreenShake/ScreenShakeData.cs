namespace V1
{
    using UnityEngine;

    [System.Serializable]
    public class ScreenShakeData
    {
        [SerializeField]
        private ScreenShakeType m_type;
        [SerializeField]
        private float m_duration;
        [SerializeField]
        private float m_amplitude;

        [SerializeField]
        private float m_maxAmplitude;

        [SerializeField]
        private float m_frequency;

        [SerializeField]
        private float m_maxFrequency;



        public float Duration
        {
            get { return m_duration; }
            set { m_duration = value; }
        }

        public float Amplitude
        {
            get { return m_amplitude; }
            set { m_amplitude = value; }
        }

        public float MaxAmplitude
        {
            get { return m_maxAmplitude; }
            set { m_maxAmplitude = value; }
        }

        public float Frequency
        {
            get { return m_frequency; }
            set { m_frequency = value; }
        }

        public float MaxFrequency
        {
            get { return m_maxFrequency; }
            set { m_maxFrequency = value; }
        }

        public ScreenShakeType ShakeType
        {
            get {return m_type;}
            set {m_type = value;}
        }
    }

    public enum ScreenShakeType
    {
        SET,
        ADD
    }
}