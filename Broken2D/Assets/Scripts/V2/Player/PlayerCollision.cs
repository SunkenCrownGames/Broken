namespace V2
{
    using UnityEngine;
    using Cinemachine;
    public class PlayerCollision : MonoBehaviour
    {
        private Entity m_entity;

        private PlayerEntity m_pe;

        [SerializeField]
        private CinemachineVirtualCamera m_virtualCamera = null;

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
                m_pe.EntityData.GM.CurrencyEvent.Invoke(energyData.EnergyValue);
                other.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D p_other) 
        {
            if(p_other.gameObject.CompareTag("Enemy"))
            {
                m_ssm.ShakeEvent.Invoke(m_pe.PlayerData.ScreenShakeHitData);
            }
        }

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
            m_cinemachineMultiChannelPerlin = m_virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            m_pe = GetComponent<PlayerEntity>();

            GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
            m_gm = gameManagerObj.GetComponent<GameManager>();
            m_ssm = gameManagerObj.GetComponent<ScreenShakeManager>();
        }
    }
}