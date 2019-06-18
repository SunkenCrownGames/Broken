using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V2
{
[System.Serializable]
public class AbilityCostData
{
        [SerializeField]
        private float m_dashCost;
        [SerializeField]
        private float m_movementCost;
        [SerializeField]
        private float m_jumpCost;
        [SerializeField]
        private float m_attackCost;
        [SerializeField]
        private float m_ability1Cost;
        [SerializeField]
        private float m_ability2Cost;
        

        public float DashCost
        {
            get { return m_dashCost; }
            set { m_dashCost = value; }
        }

        public float MovementCost
        {
            get { return m_movementCost; }
            set { m_movementCost = value; }
        }

        public float JumpCost
        {
            get { return m_jumpCost; }
            set { m_jumpCost = value; }
        }

        public float AttackCost
        {
            get { return m_attackCost; }
            set { m_attackCost = value; }
        }

        public float Ability1Cost
        {
            get { return m_ability1Cost; }
            set { m_ability1Cost = value; }
        }

        public float Ability2Cost
        {
            get { return m_ability2Cost; }
            set { m_ability2Cost = value; }
        }
    }
}