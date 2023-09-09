using Core.UI;
using UnityEngine;

namespace Code.Core.UI.Abstraction
{
    public class CanvasBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            CanvasManager.Instance.AddCanvas(this);
        }

        protected virtual void Start()
        {
        }

        public virtual void OpenCanvas()
        {
            gameObject.SetActive(true);
        }

        public virtual void CloseCanvas()
        {
            gameObject.SetActive(false);
        }

        public virtual void UpdateCanvas()
        {
        }
    }
}