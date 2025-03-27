using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEffectFader : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float m_alpha = 0.5f;
    [SerializeField] Material m_material;
    [SerializeField] string m_materialProperty = "_Alpha";
    [SerializeField] Renderer m_renderer;

    private Material m_currentMaterial;
    private float m_currentAlpha = 0f;

    void Start()
    {
        // Khởi tạo vật liệu và renderer
        m_currentMaterial = new Material(m_material);
        m_renderer.material = m_currentMaterial;
        m_currentMaterial.SetFloat(m_materialProperty, m_currentAlpha);
    }

    private void OnDidApplyAnimationProperties()
    {
        if (m_alpha != m_currentAlpha)
        {
            m_currentAlpha = Mathf.Clamp01(m_alpha);
            m_currentMaterial.SetFloat(m_materialProperty, m_currentAlpha);  // Áp dụng giá trị alpha cho vật liệu
        }
    }


    #if UNITY_EDITOR
    void OnValidate(){
        if(m_currentMaterial == null){
            m_currentMaterial = new Material(m_material);
            m_renderer.material = m_currentMaterial;
        }
        m_currentMaterial.SetFloat(m_materialProperty, m_alpha);
    }
#endif
}
