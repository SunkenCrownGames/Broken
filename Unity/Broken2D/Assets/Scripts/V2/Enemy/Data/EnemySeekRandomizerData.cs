namespace V2
{
    using UnityEngine;

    [System.Serializable]
    public class EnemySeekRandomizerData
    {
        [SerializeField]
        private Range m_horizontalSpeedRange;

        [SerializeField]
        private Range m_VerticalSpeedRange;

        [SerializeField]
        private Range m_SinWaveRange;

        [SerializeField]
        private float m_horizontalSpeed = 0;


        [SerializeField]
        private float m_verticalSpeed = 0;

        [SerializeField]
        private float m_sinWaveSpeed = 0;


        public EnemySeekRandomizerData()
        {
            m_horizontalSpeedRange = new Range();
            m_VerticalSpeedRange = new Range();
            m_SinWaveRange = new Range();
        }


        public void GenerateRandomizedData()
        {
            m_horizontalSpeed = Random.Range(m_horizontalSpeedRange.MinRange, m_horizontalSpeedRange.MaxRange);
            m_verticalSpeed = Random.Range(m_VerticalSpeedRange.MinRange, m_VerticalSpeedRange.MaxRange);
            m_sinWaveSpeed = Random.Range(m_SinWaveRange.MinRange, m_SinWaveRange.MaxRange);
        }

        public float HorinzontalSpeedRandomized
        {
            get { return m_horizontalSpeed; }
            set {  m_horizontalSpeed = value; }
        }

        public float VerticalSpeedRandomized
        {
            get { return m_verticalSpeed; }
            set { m_verticalSpeed = value; }
        }

        public float SinWaveSpeedRandomized
        {
            get { return m_sinWaveSpeed; }
            set { m_sinWaveSpeed = value; }
        }
    }
}