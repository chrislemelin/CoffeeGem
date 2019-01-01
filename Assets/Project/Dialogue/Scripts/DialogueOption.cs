using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
namespace Placeholdernamespace.Dialouge
{
    [System.Serializable]
    public class DialogueOption
    {
        public string id;
        public string displayText;
        public delegate void onOptionChoose();
        public onOptionChoose onChoose;
     
        public List<DialogueFrame> addFrames;

        public void OnMouseUp()
        {
            onChoose?.Invoke();
        }
    }
}