using System.Collections.Generic;

namespace CTM.Core.Domain.Dictionary
{
    public class DictionaryType : BaseEntity
    {
        private ICollection<DictionaryInfo> _dictionaryInfos;

        public string Name { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<DictionaryInfo> DictionaryInfos
        {
            get { return _dictionaryInfos ?? (_dictionaryInfos = new List<DictionaryInfo>()); }
            protected set { _dictionaryInfos = value; }
        }
    }
}