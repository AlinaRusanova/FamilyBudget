namespace FamilyFinance.UI.Pages
{
    public class PageItem
    {
        public string Text { get; set; }
        public int PageIndex { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }

        public PageItem(int pageIndex, bool enabled, string text)
        {
            PageIndex = pageIndex;
            Enabled = enabled;
            Text = text;
        }
    }
}
