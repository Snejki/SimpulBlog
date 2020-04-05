using SimpulBlog.Core.Enums;
using SimpulBlog.Core.Exceptions;
using SimpulBlog.Core.Extensions;
using SimpulBlog.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;

namespace SimpulBlog.Domain.Entities.Concrete
{
    public class User : Entity, IDeleteAble
    {
        public string Email { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public string Description { get; private set; }
        public string AvatarFileName { get; private set; }
        public string Hash { get; private set; }
        public string Salt { get; private set; }
        public string ResetPasswordToken { get; private set; }
        public string EmailToken { get; private set; }
        public bool IsActive { get; set; }
        public UserRole UserRole { get; private set; }

        public DateTime AddedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public virtual ICollection<Article> Articles { get; private set; }
        public virtual ICollection<UserSocialMedia> UserSocialMedia { get; set; }

        protected User() { }

        public User(string email, string firstname, string lastname, string hash, string salt, UserRole userRole, DateTime addedAt)
        {
            SetEmail(email);
            SetFirstname(firstname);
            SetLastname(lastname);
            SetPassword(hash, salt);
            SetUserRole(userRole);
            SetAddedAt(addedAt);
            SetActive(true);
        }

        public void ChangePassword(string hash, string salt)
        {
            SetPassword(hash, salt);
        }

        private void SetPassword(string hash, string salt)
        {
            if(string.IsNullOrEmpty(hash) || string.IsNullOrEmpty(salt))
                throw new BlogException(ErrorCode.EntityValidationException, "Password is wrong");

            Hash = hash;
            Salt = salt;
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrEmpty(email) || !email.IsValiDEmailAddress())
                throw new BlogException(ErrorCode.EntityValidationException, "Email is wrong");           

            Email = email;
        }

        private void SetFirstname(string firstname)
        {
            if(string.IsNullOrEmpty(firstname) || !firstname.IsOnlyLetters())
                throw new BlogException(ErrorCode.EntityValidationException, "Firstname is wrong");

            Firstname = firstname;
        }

        private void SetLastname(string lastname)
        {
            if (string.IsNullOrEmpty(lastname) || !lastname.IsOnlyLetters())
                throw new BlogException(ErrorCode.EntityValidationException, "lastname is wrong");

            Lastname = lastname;
        }

        private void SetAddedAt(DateTime addedAt)
        {
            if(addedAt > DateTime.UtcNow || addedAt == DateTime.MinValue)
                throw new BlogException(ErrorCode.EntityValidationException, "Date is wrong");

            AddedAt = addedAt;
        }

        private void SetUserRole(UserRole userRole)
        {
            UserRole = userRole;
        }

        private void SetActive(bool active)
        {
            IsActive = active;
        }      
    }
}
