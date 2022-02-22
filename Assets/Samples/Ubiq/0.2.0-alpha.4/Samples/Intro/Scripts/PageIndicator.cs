using UnityEngine;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public abstract class PageIndicator : MonoBehaviour
    {
        public abstract int capacity { get; protected set; }
        public abstract int page { get; protected set; }
        public abstract int pageCount { get; protected set; }
        public abstract void SetPageIndication (int page, int pageCount);
    }
}
