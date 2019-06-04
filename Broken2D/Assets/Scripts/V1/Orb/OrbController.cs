namespace V1
{
    using UnityEngine;
    
    public class OrbController : MonoBehaviour
    {

        [SerializeField]
        private OrbType m_orbType;

        private static GameObject m_player;

        private void Awake() 
        {
            BindObjects();
        }

        private void BindObjects()
        {
            m_player = GetComponent<GameObject>();
            m_orbType = OrbType.STATIONARY;
        }

        private void Update()
        {
            if(m_orbType == OrbType.TRACKING)
                Track();
        }

        private void Track()
        {
            
        }

        
    }
}