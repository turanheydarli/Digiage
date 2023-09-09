using System.Collections.Generic;
using System.Linq;
using Code.Core.UI.Abstraction;
using Core.Utilities.Singletons;

namespace Core.UI
{
    public class CanvasManager : SingletonBase<CanvasManager>
    {
        public List<CanvasBase> canvases = new List<CanvasBase>();

        public void OpenCanvas<TCanvas>() where TCanvas : CanvasBase
        {
            CanvasBase canvasBase = GetCanvas<TCanvas>();
            canvasBase.OpenCanvas();
        }

        public void CloseCanvas<TCanvas>() where TCanvas : CanvasBase
        {
            CanvasBase canvasBase = GetCanvas<TCanvas>();
            canvasBase.CloseCanvas();
        }

        public T GetCanvas<T>() where T : CanvasBase
        {
            return canvases.FirstOrDefault(c => c.GetType() == typeof(T)) as T;
        }

        public void AddCanvas(CanvasBase canvas)
        {
            canvases.Add(canvas);
        }

        public void RemoveCanvas(CanvasBase canvas)
        {
            canvases.Remove(canvas);
        }

        public CanvasBase UpdateCanvas<TCanvas>()
        {
            CanvasBase canvas = canvases.Single(c => c is TCanvas);

            canvas.UpdateCanvas();

            return canvas;
        }

        public void UpdateCanvases()
        {
            foreach (var item in canvases)
            {
                item.UpdateCanvas();
            }
        }
    }
}