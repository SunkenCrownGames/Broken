namespace V1
{
    using UnityEngine;
    using Cinemachine;
    public class PlayerCollision : MonoBehaviour
    {
        private Entity m_entity;

        private PlayerController m_pc;

        [SerializeField]
        private CinemachineVirtualCamera m_virtualCamera;

        private CinemachineBasicMultiChannelPerlin m_cinemachineMultiChannelPerlin;

        private GameManager m_gm;

        private ScreenShakeManager m_ssm;

        private void Awake() 
        {
            BindObjects();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("CurrencyOrb"))
            {
                EntityEnergy energyData = other.gameObject.GetComponent<EntityEnergy>();
                m_entity.GameManagerRef.CurrencyEvent.Invoke(energyData.EnergyValue);
                other.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D p_other) 
        {
            if(p_other.gameObject.CompareTag("Enemy"))
            {
                m_ssm.ShakeEvent.Invoke(m_pc.PlayerData.ShakeHitData);
            }
        }

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
            m_cinemachineMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            m_pc = GetComponent<PlayerController>();

            GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
            m_gm = gameManagerObj.GetComponent<GameManager>();
            m_ssm = gameManagerObj.GetComponent<ScreenShakeManager>();
        }
    }
}