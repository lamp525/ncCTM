using System.Collections.Generic;
using CTM.Core.Domain.Dictionary;

namespace CTM.Services.Dictionary
{
    public partial interface IDictionaryService : IBaseService
    {
        int GetDictionaryTypeIdByName(string name);

        IList<DictionaryType> GetAllDictionaryTypes();

        string GetDictionaryInfoName(int infoCode, int typeId);

        void AddDictionaryType(DictionaryType typeEntity);

        IList<DictionaryInfo> GetDictionaryInfoByTypeId(int typeId);

        IList<DictionaryInfo> GetAllDictinaryInfo();

        bool IsExistedDictionaryInfo(int typeId, string name);

        void AddDictionaryInfo(DictionaryInfo infoEntity);
    }
}