using Business.Abstract;
using Business.Constant;
using Business.Logics;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.Concrete;
using Core.Utilities;
using Core.Utilities.Business;
using Core.Utilities.Security;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        [ValidationAspect(typeof(UserForRegisterDTOValidator))]
        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }


            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
        [ValidationAspect(typeof(UserForLoginDTOValidator))]
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var result = BusinessRules.Run(CommonLogics.SystemMaintenanceTime());
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }


            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

             if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

             return new SuccessDataResult<User>(userToCheck.Data, Messages.SuccessfulLogin);
        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
