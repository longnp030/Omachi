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
        object CheckCar(Guid id);
        void AddAndOrUpdateCar(Guid id, CarModel model);
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
            var user = _context.User.SingleOrDefault(x => x.Email == model.Email);

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
            return _context.User;
        }

        public User GetById(Guid id)
        {
            return GetUser(id);
        }

        public object CheckCar(Guid id)
        {
            var car = _context.Car.SingleOrDefault(c => c.UserId == id);
            if (car != null)
            {
                return car;
            }
            return false;
        }

        public void AddAndOrUpdateCar(Guid id, CarModel model)
        {
            var tmpCar = _context.Car.SingleOrDefault(c => c.UserId == id);
            if (tmpCar == null)
            {
                var car = new Car
                {
                    Id = Guid.NewGuid(),
                    UserId = id,
                    Name = model.Name,
                    Number = model.Number,
                    Color = model.Color
                };
                _context.Car.Add(car);
                _context.SaveChanges();
            }
            else
            {
                _mapper.Map(model, tmpCar);
                _context.Car.Update(tmpCar);
                _context.SaveChanges();
            }
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.User.Any(x => x.Email == model.Email))
            {
                throw new AppException("Username '" + model.Email + "' is already taken");
            }

            if (!Validation.IsValidEmail(model.Email))
            {
                throw new AppException("Email is not valid");
            }

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // join date
            user.Timestamp = DateTime.Now;

            // save user
            _context.User.Add(user);
            _context.SaveChanges();

            
        }

        public void Update(Guid id, UpdateRequest model)
        {
            var user = GetUser(id);

            // copy model to user and save
            _mapper.Map(model, user);
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var user = GetUser(id);
            _context.User.Remove(user);
            _context.SaveChanges();
        }

        // helper methods

        private User GetUser(Guid id)
        {
            var user = _context.User.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            return user;
        }
    }
}
