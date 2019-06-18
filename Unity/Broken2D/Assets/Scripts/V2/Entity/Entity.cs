namespace V2
{
    using UnityEngine;
    //using Newtonsoft.Json;

    public class Entity : MonoBehaviour
    {
        [Header("Entity File Settings")]
        [SerializeField]
        private bool m_entityloadFromFile;

        [SerializeField]
        private TextAsset m_entityDataFile;

        [Header("Entity Data Settings")]
        [SerializeField]
        protected EntityData m_entityData;

        [SerializeField]
        protected EntityLogicData m_entityLogicData;

        protected virtual void Awake()
        {
            LoadData();
            BindObjects();
        }

        protected virtual void Update()
        {
            CheckGrounded();
            UpdateMovementState();
        }

        private void BindObjects()
        {
            if(m_entityLogicData == null)
                m_entityLogicData = new EntityLogicData();
                
            m_entityLogicData.Bounds = GetComponent<Bounds>();
            m_entityLogicData.GroundLayerMask = LayerMask.GetMask("ENV");
            m_entityLogicData.GM = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }

        private void LoadData()
        {
            if(m_entityDataFile != null && m_entityloadFromFile)
            {
                //EntityData data = JsonConvert.DeserializeObject<EntityData>(m_entityDataFile.text);
                //m_entityData = data;
                Debug.Log("Entity Data Loaded From File");
            }
        }

        private void Grounded(GameObject p_object)
        {
            m_entityLogicData.GroundObject = p_object;
        }

        //This Function Checks If The Player Has Hit The Ground By Firing a Ray Downwards
        private void CheckGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, m_entityLogicData.GroundLayerMask);

            if (hit.collider != null && transform.position.y > hit.point.y)
            {
                GameObject hitObject = hit.collider.gameObject;
                Bounds objectBounds = hitObject.GetComponent<Bounds>();
                m_entityData.CurrentGroundDistance = m_entityLogicData.Bounds.EntityVerticalBounds[0] - objectBounds.EntityVerticalBounds[1];

                if(m_entityLogicData.Bounds.EntityVerticalBounds[1] > objectBounds.EntityVerticalBounds[0])
                {
                    if(hit.distance < m_entityData.GroundedDistance && m_entityData.MovementState == EntityMovementState.FALLING && m_entityData.VDirection == VerticalDirection.DOWN)
                    {
                        //Debug.Log("SET GROUNDED");
                        if(hitObject.CompareTag("Ground"))
                        {
                            Grounded(hitObject);
                        }
                    }
                    else if(hit.distance > m_entityData.GroundedDistance)
                    {
                        Grounded(null);
                    }
                }
            }
            else
            {
                Grounded(null);
            }
        }

        //This Function Updates the Movement State Depending on the Air State Of the Entity
        private void UpdateMovementState()
        {
                //If we are not grounded
                if(m_entityLogicData.GroundObject == null)
                {
                    //If we are not jumping
                    if(m_entityData.MovementState != EntityMovementState.JUMPING)
                        m_entityData.MovementState = EntityMovementState.FALLING;
                }
                //if we are not currently jumping
                else if(m_entityData.MovementState == EntityMovementState.FALLING)
                {
                    m_entityData.MovementState = EntityMovementState.GROUNDED;
                }
        }
        
        public string SerializedEntityData()
        {
            //return JsonConvert.SerializeObject(m_entityData);
            return "NULL";
        }

        public EntityData EntityData
        {
            get { return m_entityData; }
            set { m_entityData = value; }
        }

        public EntityLogicData LogicData
        {
            get { return m_entityLogicData; }
            set { m_entityLogicData = value; }
        }
    }
}