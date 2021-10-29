using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using おマチ.API.Authorization;
using おマチ.API.Entities;
using おマチ.API.Helpers;
using おマチ.API.Models.User;

namespace おマチ.API.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Register(RegisterRequest model);
        void Update(Guid id, UpdateRequest model);
        void Delete(Guid id);
    }

    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IJwtUtils jwtUtils, IMapper mapper)
        {
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                throw new AppException("Username or Password is incorrect");
            }

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.JwtToken = _jwtUtils.GenerateToken(user);
            return response;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetById(Guid id)
        {
            return GetUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Email == model.Email))
            {
                throw new AppException("Username '" + model.Email + "' is already taken");
            }

            if (!Validation.IsValidEmail(model.Email))
            {
                throw new AppException("Email is not valid");
            }

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // send notification email
            new EmailService(user.Email, "おマチへようこそ！", "アカウントご登録ありがとうございます！　楽しい時間をお過ごしできますように！").Send();

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // join date
            user.JoinDate = DateTime.Now;

            // save user
            _context.Users.Add(user);
            _context.SaveChanges();

            
        }

        public void Update(Guid id, UpdateRequest model)
        {
            var user = GetUser(id);

            // validate
            if (model.UserName != user.UserName && _context.Users.Any(x => x.UserName == model.UserName))
            {
                throw new AppException("Username '" + model.UserName + "' is already taken");
            }

            // hash password if it was entered
            if (!String.IsNullOrEmpty(model.Password))
            {
                user.PasswordHash = BCryptNet.HashPassword(model.Password);
            }

            // copy model to user and save
            _mapper.Map(model, user);
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = GetUser(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User GetUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }
    }
}
