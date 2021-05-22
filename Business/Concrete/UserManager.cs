using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Logics;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Aspects.Autofac.Caching;
using Core.Concrete;
using Core.Utilities;
using Core.Utilities.Business;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        [ValidationAspect(typeof(UserValidator))]
        [SecuredOperation("user")]
        [CacheRemoveAspect("IUserService.Get")]
        public IResult Add(User user)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime(),UserLogics.CheckIfEmailAlreadyExist(_userDal,user.Email));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _userDal.Add(user);
             return new SuccessResult();
        }
        [CacheAspect(duration:20)]
        public IDataResult<User> GetByMail(string email)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }

            return new SuccessDataResult<User>(_userDal.Get(u => u.Email == email));
        }
        [CacheAspect]
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<List<OperationClaim>>(result.Message);
            }
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }



     


    }
}
