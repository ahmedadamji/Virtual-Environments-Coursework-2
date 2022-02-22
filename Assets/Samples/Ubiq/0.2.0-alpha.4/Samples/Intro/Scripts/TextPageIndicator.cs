using UnityEngine.UI;

namespace Samples.Ubiq._0._2._0_alpha._4.Samples.Intro.Scripts
{
    public sealed class TextPageIndicator : PageIndicator
    {
        public Text pageText;
        public Text pageCountText;

        public override int capacity { get => int.MaxValue; protected set {} }
        public override int page { get; protected set; }
        public override int pageCount { get; protected set; }

        public override void SetPageIndication (int page, int pageCount)
        {
            if (this.pageCount != pageCount)
            {
                pageCountText.text = pageCount.ToString();
                this.pageCount = pageCount;
            }

            if (this.page != page)
            {
                pageText.text = (page+1).ToString();
                this.page = page;
            }
        }
    }
}
