using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AngleDebugger : MonoBehaviour
{
    public GameObject m_startObject;

    public Vector3 m_endPositionOffset;

    private Vector3 m_endPoint;
    private Vector3 m_startPoint;

    private float currentAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(m_startObject != null)
        {
            CheckChange();
            UpdateAngle();

            if(currentAngle != UpdateAngle())
            {
                currentAngle = UpdateAngle();
                Debug.Log(currentAngle);
            }
        }

    }

    private void CheckChange()
    {
        if(m_startObject.transform.position != m_startPoint)
            m_startPoint = m_startObject.transform.position;

        if(m_endPoint != m_startPoint + m_endPositionOffset)
            m_endPoint = m_startPoint + m_endPositionOffset;
    }

    private float UpdateAngle()
    {
        return Mathf.Rad2Deg * Mathf.Atan2(m_endPoint.y, m_endPoint.x);
    }
}
