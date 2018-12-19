using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationShots.BLL
{
    public class SearchResult
    {
        public string ResultName { get; set; }
        public string ResultUrl { get; set; }
        public string ResultFolder { get; set; }

        public SearchResult(DataRow dataRow)
        {
            ResultName = dataRow.Field<string>(1);
            ResultUrl = dataRow.Field<string>(0);
        }
        public SearchResult(DataRow dataRow, Func<string, string> extractUrl): this(dataRow)
        {
            ResultUrl = extractUrl(ResultUrl);
        }

        public override bool Equals(object obj)
        {
            var other = obj as SearchResult;
            if (other == null)
                return false;

            var res = 1 == 1
                && this.ResultName == other.ResultName
                && this.ResultUrl == other.ResultUrl;

            return res;
        }
        public override int GetHashCode()
        {
            return -13;
        }
        public override string ToString()
        {
            return ResultName;
        }
    }
}
