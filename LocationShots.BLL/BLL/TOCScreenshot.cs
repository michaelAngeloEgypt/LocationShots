using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace LocationShots.BLL
{
    public class TOCScreenshot
    {
        public string Filename { get; set; }
        public List<TOCChoice> Choices { get; set; }
        public List<TOCChoice> FilteredChoices
        {
            get
            {
                if (Choices == null || Choices.Count == 0) return new List<TOCChoice>();
                else return Choices.Where(c => c.Ticked == true.ToString()).ToList();
            }
        }
        public TOCScreenshot()
        {
            Choices = new List<TOCChoice>();
        }
        public override string ToString()
        {
            var promptWidth = nameof(FilteredChoices).Length + 3;
            string res = 
$@"{Strings.StrDup(1,'\t')}(
{Strings.StrDup(2, '\t')}{nameof(Filename) + ":".PadRight(promptWidth)}{Filename}
{Strings.StrDup(1, '\t')})";
            return res;
        }
    }
    public class TOCChoice
    {
        public string By { get; set; }
        public string Value { get; set; }
        public string Level0 { get; set; }
        public string Level1 { get; set; }
        public string Level2 { get; set; }
        public string Level3 { get; set; }
        public string Ticked { get; set; }
        public string ChoiceText
        {
            get
            {
                string res = $"{Level0 ?? ""}";
                res = string.IsNullOrWhiteSpace(res)? $"{Level1 ?? ""}":res;
                res = string.IsNullOrWhiteSpace(res)? $"{Level2 ?? ""}":res;
                res = string.IsNullOrWhiteSpace(res)? $"{Level3 ?? ""}":res;

                return res;
            }
        }
        public override string ToString()
        {
            var Text = Level3;
            if (string.IsNullOrWhiteSpace(Text))
                Text = Level2;
            if (string.IsNullOrWhiteSpace(Text))
                Text = Level1;
            if (string.IsNullOrWhiteSpace(Text))
                Text = Level0;

            var promptWidth = nameof(Ticked).Length + 3;
            string res =
$@"{Strings.StrDup(1, '\t')}(
{Strings.StrDup(2, '\t')}{nameof(Text) + ":".PadRight(promptWidth)}{Text}
{Strings.StrDup(2, '\t')}{nameof(By) + ":".PadRight(promptWidth)}{By}
{Strings.StrDup(2, '\t')}{nameof(Value) + ":".PadRight(promptWidth)}{Value}
{Strings.StrDup(1, '\t')})";
            return res;
        }
    }
}
