/* Script to transision between snapshots dynamically based on how far
 * through a trigger the player is.
 * 
 * Copyright (C) 2017 Christian Schubert
 * This script is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This script is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this script.  If not, see <http://www.gnu.org/licenses/>.
 */

// https://www.youtube.com/user/BjarneBiceps
// http://cujo.dk/scripts/


using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(BoxCollider))]
public class AdvancedSnapshotCrossfade : MonoBehaviour
{
    private const float DEFAULT_VALUE_MIN = 0.0f;
    private const float DEFAULT_VALUE_MAX = 1.0f;

    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private AudioMixerSnapshot m_AudioMixerSnapshot0;
    [SerializeField] private AudioMixerSnapshot m_AudioMixerSnapshot1;

    [SerializeField] private Vector3 m_ProgressionDirection;
    [SerializeField] private float m_Smoothing;
    [SerializeField] private bool m_RecalulateOnUpdate;

    private BoxCollider m_BoxCollider;
    private Vector3 m_CorrectedProgressionDirection;

    private Vector3 m_MinDirectionBound = Vector3.zero;
    private Vector3 m_MaxDirectionBound = Vector3.zero;
    private Vector3 m_WorldMin = Vector3.zero;
    private Vector3 m_WorldMax = Vector3.zero;
    private Vector3 m_Progression;
    private float m_MinMaxDistance;
    private float m_Value;

    private void Recalculate()
    {
        m_CorrectedProgressionDirection = (transform.rotation * m_ProgressionDirection.normalized).normalized;
        if (m_BoxCollider == null)
            return;
        m_MinDirectionBound = m_BoxCollider.center - transform.rotation * new Vector3(m_BoxCollider.size.x / 2 * m_ProgressionDirection.x, m_BoxCollider.size.y / 2 * m_ProgressionDirection.y, m_BoxCollider.size.z / 2 * m_ProgressionDirection.z);
        m_MaxDirectionBound = m_BoxCollider.center + transform.rotation * new Vector3(m_BoxCollider.size.x / 2 * m_ProgressionDirection.x, m_BoxCollider.size.y / 2 * m_ProgressionDirection.y, m_BoxCollider.size.z / 2 * m_ProgressionDirection.z);
        m_WorldMin = new Vector3((transform.position.x + m_MinDirectionBound.x) * m_CorrectedProgressionDirection.x, (transform.position.y + m_MinDirectionBound.y) * m_CorrectedProgressionDirection.y, (transform.position.z + m_MinDirectionBound.z) * m_CorrectedProgressionDirection.z);
        m_WorldMax = new Vector3((transform.position.x + m_MaxDirectionBound.x) * m_CorrectedProgressionDirection.x, (transform.position.y + m_MaxDirectionBound.y) * m_CorrectedProgressionDirection.y, (transform.position.z + m_MaxDirectionBound.z) * m_CorrectedProgressionDirection.z);
        m_MinMaxDistance = Vector3.Distance(m_WorldMin, m_WorldMax);
    }

    private void SetAudioMixer()
    {
        m_AudioMixer.TransitionToSnapshots(new[] { m_AudioMixerSnapshot0, m_AudioMixerSnapshot1 }, new[] { 1 - m_Value, m_Value }, m_Smoothing);
    }

    public void Start()
    {
        m_BoxCollider = GetComponent<BoxCollider>();
        m_Value = DEFAULT_VALUE_MIN;
        SetAudioMixer();
        Recalculate();
    }

    public void Update()
    {
        if (m_RecalulateOnUpdate)
            Recalculate();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other == null || !other.tag.Equals("Player") || m_BoxCollider == null)
            return;

        Vector3 otherPosition = other.transform.position;
        m_Progression = new Vector3(otherPosition.x * m_CorrectedProgressionDirection.x, otherPosition.y * m_CorrectedProgressionDirection.y, otherPosition.z * m_CorrectedProgressionDirection.z);
        m_Value = Mathf.Clamp01(Vector3.Distance(m_WorldMin, m_Progression) / m_MinMaxDistance); // This is the % amount from min in 0..1
        SetAudioMixer();
        //Debug.Log(progression + " " + m_WorldMin + " " + m_WorldMax + " " + distToMin);
    }

    public void OnTriggerExit(Collider other)
    {
        m_Value = DEFAULT_VALUE_MAX - m_Value > m_Value - DEFAULT_VALUE_MAX ? DEFAULT_VALUE_MIN : DEFAULT_VALUE_MAX;
        SetAudioMixer();
    }

    public void OnDrawGizmosSelected()
    {
        if (m_RecalulateOnUpdate)
            Recalculate();
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, transform.position + m_CorrectedProgressionDirection);
        Gizmos.DrawSphere(transform.position + m_CorrectedProgressionDirection, 0.1f);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + m_MinDirectionBound);
        Gizmos.DrawSphere(transform.position + m_MinDirectionBound, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + m_MaxDirectionBound);
        Gizmos.DrawSphere(transform.position + m_MaxDirectionBound, 0.1f);
    }
}