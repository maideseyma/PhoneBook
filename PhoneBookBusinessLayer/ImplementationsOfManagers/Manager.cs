using AutoMapper;
using PhoneBookBusinessLayer.InterfacesOfManagers;
using PhoneBookDataLayer.InterfacesOfRepo;
using PhoneBookEntityLayer.Entities.Mappings.ResultModels;
using System;
using System.Linq.Expressions;

namespace PhoneBookBusinessLayer.ImplementationsOfManagers
{
    public class Manager<TViewModel, TModel, Id> : IManager<TViewModel, Id>
        where TViewModel : class, new()
        where TModel : class, new()
    {
        protected readonly IRepository<TModel, Id> _repo;
        protected readonly IMapper _mapper;
        protected readonly string _includeRelationalTables;

        public Manager(IRepository<TModel, Id> repo, IMapper mapper, string includeRelationalTables)
        {
            _repo = repo;
            _mapper = mapper;
            _includeRelationalTables = includeRelationalTables;
        }

        public IDataResult<TViewModel> Add(TViewModel model)
        {
            try
            {
                //bize parametre olarak gelen DTO'yu repoya gönderemiyoruz
                //repoya modelin kendisi gönderilmelidir bu nedenle mapper ile dönüşüm yaptık

                TModel tmodel = _mapper.Map<TViewModel, TModel>(model);
                int result = _repo.Add(tmodel); // tmodel repo ile veritabanına eklendi t modelin artık idsi var

                TViewModel dataModel = _mapper.Map<TModel, TViewModel>(tmodel);
                return result > 0 ? new DataResult<TViewModel>(success: true, message: "Ekleme işlemi başarılıdır", data: model) :
                    new DataResult<TViewModel>(model, "Ekleme işlemi BŞARISIZDIR!", false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IResult Delete(TViewModel model)
        {
            try
            {
                TModel tmodel = _mapper.Map<TViewModel, TModel>(model);
                if (_repo.Delete(tmodel) > 0)
                {
                    return new Result(true, "Silme işlemi gerçekleşti.");
                }
                else
                {
                    return new Result(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IDataResult<ICollection<TViewModel>> GetAll(Expression<Func<TViewModel, bool>>? filter = null)
        {
            try
            {
                var fltr = _mapper.Map<Expression<Func<TViewModel, bool>>,
                                       Expression<Func<TModel, bool>>>(filter);


                var data = _repo.GetAll(fltr, _includeRelationalTables.Split(","));

                ICollection<TViewModel> dataList =
                    _mapper.Map<IQueryable<TModel>, ICollection<TViewModel>>(data);

                return new DataResult<ICollection<TViewModel>>(dataList, "", true);

            }
            catch (Exception)
            {

                throw;
            }
        }


        public IDataResult<TViewModel> GetByConditions(Expression<Func<TViewModel, bool>>? filter = null)
        {
            try
            {
                var fltr = _mapper.Map<Expression<Func<TViewModel, bool>>,
                                     Expression<Func<TModel, bool>>>(filter);

                var data = _repo.GetByConditions(fltr, _includeRelationalTables.Split(","));
                if (data == null)
                {
                    return new DataResult<TViewModel>(false, null);
                }

                var dataModel = _mapper.Map<TModel, TViewModel>(data);
                return new DataResult<TViewModel>(true, dataModel);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public IDataResult<TViewModel> GetById(Id id)
        {
            try
            {
                if (id==null)
                {
                    return new DataResult<TViewModel>(false, null);
                }

                var data = _repo.GetById(id);
                if (data==null)
                {
                    return new DataResult<TViewModel>(null, "KAYIT BULUNAMADI",false);
                }

                var dataModel = _mapper.Map<TModel, TViewModel>(data);
                return new DataResult<TViewModel>(dataModel, "KAYIT BULUNDU", true);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public IResult Update(TViewModel model)
        {
            try
            {
                TModel tmodel = _mapper.Map<TViewModel, TModel>(model);

                int result = _repo.Update(tmodel);
                if (result > 0)
                {
                    return new Result(true, "Güncelleme işlemi yapıldı");
                }
                else
                {
                    return new Result(false, "Güncelleme işlemi BAŞARISIZSIR");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
