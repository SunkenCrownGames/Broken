namespace V1
{
    using UnityEngine;
    
    public class PlayerCollision : MonoBehaviour
    {
        private Entity m_entity;


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

        private void BindObjects()
        {
            m_entity = GetComponent<Entity>();
        }
    }
}