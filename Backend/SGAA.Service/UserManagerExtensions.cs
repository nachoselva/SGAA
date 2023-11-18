namespace SGAA.Service
{
    using Microsoft.AspNetCore.Identity;
    using SGAA.Domain.Auth;
    using SGAA.Domain.Errors;
    using SGAA.Models;
    using System.Collections.Generic;

    public static class UserManagerExtensions
    {
        public static BadRequestException MapIdentityErrorToBadRequest(this UserManager<Usuario> userManager, IEnumerable<IdentityError> errors)
        {
            BadRequestException badRequestException = new();
            foreach (IdentityError error in errors)
            {
                string fieldName = error.Code switch
                {
                    "InvalidUserName" or "InvalidEmail" or "DuplicateUserName" or "DuplicateEmail" => nameof(UsuarioPostModel.Email),
                    "InvalidRoleName" or "DuplicateRoleName" => nameof(Rol),
                    "UserAlreadyHasPassword" or "UserLockoutNotEnabled" or "UserAlreadyInRole" or "UserNotInRole" or "ConcurrencyFailure" or "LoginAlreadyAssociated" => nameof(Usuario),
                    "InvalidToken" or "PasswordTooShort" or "PasswordRequiresNonAlphanumeric" or "PasswordRequiresDigit" or "PasswordRequiresLower" or "PasswordRequiresUpper" or "PasswordMismatch" => nameof(UsuarioPostModel.Password),
                    _ => "Unknown",
                };
                badRequestException.AddMessage(fieldName, error.Description);
            }
            return badRequestException;
        }
    }
}
