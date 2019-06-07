namespace V1
{
    using UnityEngine;
    using Cinemachine;
    public class ScreenShakeManager : MonoBehaviour
    {
        private ScreenShakeEvent m_screenShakeEvent;
        [SerializeField]
        private CinemachineVirtualCamera m_virtualCamera;


        private CinemachineBasicMultiChannelPerlin m_cinemachinePerlinChannel;

        private ScreenShakeData m_activeScreenShakeData;

        private float m_currentDuration = 0;

        private void Awake() 
        {
            BindEvents();
            BindObjects();
        }

        private void Update()
        {
            if(m_activeScreenShakeData != null)
                Shake();

        }

        private void BindEvents()
        {
            if(m_screenShakeEvent == null)
                m_screenShakeEvent = new ScreenShakeEvent();

            m_screenShakeEvent.AddListener(StartScreenShake);
        }

        private void BindObjects()
        {
            m_cinemachinePerlinChannel = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }

        private void StartScreenShake(ScreenShakeData p_data)
        {
            m_activeScreenShakeData = p_data;
        }

        private void Shake()
        {
            if(m_currentDuration < m_activeScreenShakeData.Duration)
            {
                if(m_activeScreenShakeData.ShakeType == ScreenShakeType.SET)
                {
                    m_cinemachinePerlinChannel.m_FrequencyGain = m_activeScreenShakeData.Frequency;
                    m_cinemachinePerlinChannel.m_AmplitudeGain = m_activeScreenShakeData.Amplitude;
                }
                else
                {
                    if(m_cinemachinePerlinChannel.m_FrequencyGain < m_activeScreenShakeData.MaxFrequency)
                        m_cinemachinePerlinChannel.m_FrequencyGain += m_activeScreenShakeData.Frequency;

                    if(m_cinemachinePerlinChannel.m_AmplitudeGain < m_activeScreenShakeData.MaxAmplitude)
                        m_cinemachinePerlinChannel.m_AmplitudeGain += m_activeScreenShakeData.Amplitude;
                }
                m_currentDuration += Time.deltaTime;
            }
            else
            {
                m_currentDuration = 0;
                m_cinemachinePerlinChannel.m_FrequencyGain = 0;
                m_cinemachinePerlinChannel.m_AmplitudeGain = 0;
                m_activeScreenShakeData = null;
            }
        }

        public ScreenShakeEvent ShakeEvent
        {
            get { return m_screenShakeEvent; }
            set { m_screenShakeEvent = value; }
        }
    }
}