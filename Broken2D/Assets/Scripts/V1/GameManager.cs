using UnityEngine;
using UnityEngine.Events;

namespace V1
{
    [ExecuteAlways]
    public class GameManager : MonoBehaviour 
    {

        public PlayerController m_playerRef;

        [SerializeField]
        private float m_startCurrency = 0.0f;
        [SerializeField]
        private bool m_updateCurrency;
        [SerializeField]
        private float m_gravityScale = 9.0f;

        [SerializeField]
        private float m_currency = 0;

        private UpdateCurrencyEvent m_currencyEvent;
        
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

            m_currencyEvent.AddListener(UpdateCurrency);
        }

        private void UpdateCurrency(float val)
        {
            m_currency += val;
            Debug.Log("Currency Updated + Current Value is: " + m_currency);

            if(m_currency <= 0)
            {
                Debug.Log("Game Over");
                Destroy(m_playerRef.gameObject);
            }
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
        
    }

    public class UpdateCurrencyEvent : UnityEvent<float> {}
}