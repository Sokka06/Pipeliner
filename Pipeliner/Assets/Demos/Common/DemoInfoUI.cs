using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Demos.Common
{
    public class DemoInfoUI : MonoBehaviour
    {
        public DemoInfo info;

        [Space] 
        public TextMeshProUGUI TitleText;
        public TextMeshProUGUI SubtitleText;
        public TextMeshProUGUI DescriptionText;
        
        private void OnValidate()
        {
            UpdateInfo();
        }

        [ContextMenu("Update Info")]
        public void UpdateInfo()
        {
            if (info == null)
                return;
            
            if (TitleText != null)
            {
                TitleText.SetText(info.Title);
            }

            if (SubtitleText != null)
            {
                SubtitleText.SetText(info.Subtitle);
            }

            if (DescriptionText != null)
            {
                DescriptionText.SetText(info.Description);
                
                var rectTransform = DescriptionText.rectTransform.parent as RectTransform;
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.RoundToInt(DescriptionText.preferredHeight));
                //Debug.Log($"{DescriptionText.preferredHeight}, {rectTransform.sizeDelta}");
            }
        }
    }
}