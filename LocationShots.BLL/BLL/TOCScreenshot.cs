using System.Collections.Generic;
using System.Linq;

namespace LocationShots.BLL
{
    public class TOCScreenshot
    {
        public string Filename { get; set; }
        public List<TOCChoices> Choices { get; set; }
        public List<TOCChoices> FilteredChoices
        {
            get
            {
                if (Choices == null || Choices.Count == 0) return new List<TOCChoices>();
                else return Choices.Where(c => c.Ticked == true.ToString()).ToList();
            }
        }
        public TOCScreenshot()
        {
            Choices = new List<TOCChoices>();
        }
    }
    public class TOCChoices
    {
        public string By { get; set; }
        public string Value { get; set; }
        public string Level0 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Ticked { get; set; }
    }
}
