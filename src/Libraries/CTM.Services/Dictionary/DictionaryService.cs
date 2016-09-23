using System;
using System.Collections.Generic;
using System.Linq;
using CTM.Core.Data;
using CTM.Core.Domain.Dictionary;

namespace CTM.Services.Dictionary
{
    public partial class DictionaryService : IDictionaryService
    {
        #region Fields

        private readonly IRepository<DictionaryType> _dictionaryTypeRepository;
        private readonly IRepository<DictionaryInfo> _dictionaryInfoRepository;

        #endregion Fields

        #region Constructors

        public DictionaryService(
            IRepository<DictionaryType> dictionaryTypeRepository,
            IRepository<DictionaryInfo> dictionaryInfoRepository)
        {
            this._dictionaryTypeRepository = dictionaryTypeRepository;
            this._dictionaryInfoRepository = dictionaryInfoRepository;
        }

        #endregion Constructors

        #region Methods

        public virtual int GetDictionaryTypeIdByName(string name)
        {
            int typeId = 0;

            var info = _dictionaryTypeRepository.Table.Where(x => x.Name == name).FirstOrDefault();

            if (info != null)
                typeId = info.Id;

            return typeId;
        }

        public virtual IList<DictionaryType> GetAllDictionaryTypes()
        {
            var result = new List<DictionaryType>();

            var query = _dictionaryTypeRepository.Table;
            query = query.OrderBy(x => x.Id);

            result = query.ToList();

            return result;
        }

        public virtual string GetDictionaryInfoName(int infoCode, int typeId)
        {
            var name = string.Empty;

            var info = _dictionaryInfoRepository.Table.Where(x => x.Code == infoCode && x.TypeId == typeId).FirstOrDefault();

            if (info != null) name = info.Name;

            return name;
        }

        public virtual IList<DictionaryInfo> GetDictionaryInfoByTypeId(int typeId)
        {
            var result = new List<DictionaryInfo>();

            var query = from i in _dictionaryInfoRepository.Table
                        where i.TypeId == typeId
                        select i;

            query = query.OrderBy(x => x.Id);

            result = query.ToList();

            return result;
        }

        public virtual IList<DictionaryInfo> GetAllDictinaryInfo()
        {
            var result = new List<DictionaryInfo>();

            var query = _dictionaryInfoRepository.Table;

            query = query.OrderBy(x => x.Id);

            result = query.ToList();

            return result;
        }

        public virtual void AddDictionaryType(DictionaryType typeEntity)
        {
            if (typeEntity == null)
                throw new ArgumentNullException(nameof(typeEntity));

            _dictionaryTypeRepository.Insert(typeEntity);
        }

        public virtual void AddDictionaryInfo(DictionaryInfo infoEntity)
        {
            if (infoEntity == null)
                throw new ArgumentNullException(nameof(infoEntity));

            var query = _dictionaryInfoRepository.Table;
            query = query.Where(x => x.TypeId == infoEntity.TypeId);

            infoEntity.Code = query.Count() + 1;

            _dictionaryInfoRepository.Insert(infoEntity);
        }

        public virtual bool IsExistedDictionaryInfo(int typeId, string name)
        {
            var query = _dictionaryInfoRepository.Table.Where(x => x.TypeId == typeId && x.Name == name);

            var info = query.FirstOrDefault();

            return info == null ? false : true;
        }

        #endregion Methods
    }
}