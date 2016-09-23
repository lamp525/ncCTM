namespace CTM.Core.Domain.Dictionary
{
    public class DictionaryInfo : BaseEntity
    {
        /// <summary>
        /// Dictionary type indentifier
        /// </summary>
        public int TypeId { get; set; }

        public int Code { get; set; }

        public string Name { get; set; }

        public string Remarks { get; set; }

        public virtual DictionaryType DictionaryType { get; set; }
    }
}