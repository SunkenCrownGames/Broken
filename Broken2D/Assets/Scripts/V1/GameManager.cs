using UnityEngine;
using TMPro;

namespace V1
{
    [ExecuteAlways]
    public class GameManager : MonoBehaviour 
    {

        public PlayerController m_playerRef;
        public TextMeshProUGUI m_currencyValDebug;
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
                    m_currencyValDebug.text = m_currency.ToString();
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
                m_currency = PlayerPrefs.GetFloat("Currency");
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

        private void BindEvents()
        {
            if(m_currencyEvent == null)
                m_currencyEvent = new UpdateCurrencyEvent();

            if(m_spawnOrbEvent == null)
                m_spawnOrbEvent = new SpawnOrbEvent();

            m_currencyEvent.AddListener(UpdateCurrency);
            m_spawnOrbEvent.AddListener(SpawnOrb);
        }

        private void UpdateCurrency(float val)
        {
            m_currency += val;
            Debug.Log("Currency Updated + Current Value is: " + m_currency);

            if(m_currency <= 0)
            {
                Debug.Log("Game Over");
                Destroy(m_playerRef.gameObject);
                m_currency = 0;
            }
        }

        private void SpawnOrb(EnemyOrbData p_orbEnemyData)
        {
            GameObject orb = Instantiate(m_orbPrefab, p_orbEnemyData.SpawnPosition, Quaternion.identity);
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