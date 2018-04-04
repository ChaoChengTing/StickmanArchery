using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour
{
    private float m_distance;
    private bool m_hasHit;
    private RaycastHit m_hit;
    private Vector3 m_mousePositionInWorld;

    private void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            m_hasHit = Physics.Raycast(ray, out m_hit);
            if (m_hasHit)
            {
                Rigidbody rigidbody = m_hit.collider.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    m_distance = Vector3.Distance(Camera.main.transform.position, m_hit.transform.position);
                    m_mousePositionInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_distance));

                    rigidbody.useGravity = false;
                    rigidbody.isKinematic = true;
                }
            }
        }

        if (m_hasHit && Input.GetMouseButton(2))
        {
            Vector3 currentMousePositionInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_distance));
            if (m_mousePositionInWorld != currentMousePositionInWorld)
            {
                m_hit.collider.transform.position += currentMousePositionInWorld - m_mousePositionInWorld;
                m_mousePositionInWorld = currentMousePositionInWorld;
            }
        }

        if (m_hasHit && Input.GetMouseButtonUp(2))
        {
            Rigidbody rigidbody = m_hit.collider.GetComponent<Rigidbody>();
            if (rigidbody != null)
            {
                rigidbody.useGravity = true;
                rigidbody.isKinematic = false;
            }
        }
    }
}