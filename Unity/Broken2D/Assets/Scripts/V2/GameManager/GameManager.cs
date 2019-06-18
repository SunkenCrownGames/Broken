using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace V2
{
    [ExecuteAlways]
    public class GameManager : MonoBehaviour 
    {

        private PlayerEntity m_playerRef;

        public TextMeshProUGUI m_currencyValDebug;

        [SerializeField]
        private ScreenShakeManager m_ssm;

        [SerializeField]
        private Slider m_slider;

        [SerializeField]
        private Slider m_slider1;

        [SerializeField]
        private TextMeshProUGUI m_arcOffset;

        [SerializeField]
        private TextMeshProUGUI m_bulletCount;
        
        public bool m_debugMode;

        [SerializeField]
        private GameObject m_orbPrefab = null;

        [SerializeField]
        private float m_startCurrency = 0.0f;

        [SerializeField]
        private bool m_updateCurrency;
        [SerializeField]
        private float m_gravityScale = 9.0f;

        [SerializeField]
        private float m_currency = 0;

        private UpdateCurrencyEvent m_currencyEvent;

        private SpawnOrbEvent m_spawnOrbEvent;

        private void Awake() 
        {
            //GAMEPLAY
            if (Application.IsPlaying(gameObject))
            {
                LoadCurrency();
                BindEvents();
                BindObjects();
            }
            //EDITOR
            else
            {

            }
        }

        private void Update()
        {
            //GAMEPLAY
            if (Application.IsPlaying(gameObject))
            {
                if(m_debugMode)
                {
                    m_currencyValDebug.text = m_currency.ToString();
                    ResetLevel();

                    
                    m_bulletCount.text = m_slider.value.ToString();
                    m_playerRef.PlayerData.ShootingData.BulletCount = (int)m_slider.value;
                    m_arcOffset.text = m_slider1.value.ToString();
                    m_playerRef.PlayerData.ShootingData.AngleOffset = m_slider1.value;
                }
            }
            //IN EDITOR
            else
            {
                UpdateStartCurrency();
            }
        }
        

        private void LoadCurrency()
        {
            if(PlayerPrefs.HasKey("Currency"))
            {
                Debug.Log("Loaded Currency");
                m_currency = 1000F;
            }
            else
            {
                Debug.Log("Created Currency Variable");
                PlayerPrefs.SetFloat("Currency", m_startCurrency);
            }
        }

        private void UpdateStartCurrency()
        {
            if(m_updateCurrency)
            {
                PlayerPrefs.SetFloat("Currency", m_startCurrency);
                Debug.Log("Start Currency Updated");
                m_updateCurrency = false;
            }
        }

        private void ResetLevel()
        {
            if(Input.GetKeyDown(KeyCode.R))
                  SceneManager.LoadScene(0);
        }

        private void BindEvents()
        {
            if(m_currencyEvent == null)
                m_currencyEvent = new UpdateCurrencyEvent();

            if(m_spawnOrbEvent == null)
                m_spawnOrbEvent = new SpawnOrbEvent();

            m_currencyEvent.AddListener(UpdateCurrency);
            m_spawnOrbEvent.AddListener(SpawnOrb);
        }

        private void BindObjects()
        {
            m_ssm = GetComponent<ScreenShakeManager>();
            m_playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEntity>();
        }

        private void UpdateCurrency(float val)
        {
            m_currency += val;
            //Debug.Log("Currency Updated + Current Value is: " + m_currency);

            if(m_currency <= 0)
            {
                Debug.Log("Game Over");
                m_ssm.ShakeEvent.Invoke(m_playerRef.EntityData.HitScreenShakeData);
                Destroy(m_playerRef.gameObject);
                m_currency = 0;
            }
        }

        private void SpawnOrb(EnemyOrbData p_orbEnemyData)
        {
            GameObject orb = Instantiate(m_orbPrefab, p_orbEnemyData.SpawnPosition.ToVector3(), Quaternion.identity);
            OrbController oc = orb.GetComponent<OrbController>();
            oc.OrbData = new OrbSpawnData(p_orbEnemyData);
        }


        public float GravityScale
        {
            get { return m_gravityScale; }
            set { m_gravityScale = value; }
        }

        public UpdateCurrencyEvent CurrencyEvent
        {
            get { return m_currencyEvent; }
            set { m_currencyEvent = value; }
        }

        public SpawnOrbEvent OrbEvent
        {
            get { return m_spawnOrbEvent; }
            set {  m_spawnOrbEvent = value; }
        }
    }
}