using AutoMapper;
using FamilyFinance.Domain.Entities.Identity;
using FamilyFinance.Domain.Repositories.IRepositories.Identity;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.Persistence.Services.IServices.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FamilyFinance.Persistence.Services.Identity
{
    public class UserService : IUserService<UserModel>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IConfiguration configuration, IMapper mapper)
        {
            if (userRepository == null || configuration == null || mapper == null)
            {
                throw new BadRequestException(nameof(UserService));
            }

            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<UserModel> Authenticate(UserModel model, CancellationToken ct)
        {
            IEnumerable<User> entities = await _userRepository.ListAllAsync(ct);
            var user = entities.FirstOrDefault(x => x.UserName == model.UserName);

            if (user == null)
            {
                throw new IncorrectUserException(model.UserName);
            }

            if (!VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            { throw new IncorrectPasswordException(); }

            var userModel = _mapper.Map<UserModel>(user);
            userModel.Token = CreateToken(user);

            return userModel;
        }

        public async Task<UserModel> Register(UserModel userModel, CancellationToken ct)
        {
            var user = _mapper.Map<User>(userModel);

            var list = await _userRepository.ListAllAsync(ct);

            if (list.Any(x => x.UserName == userModel.UserName))
            {
                throw new NotUniqueUsernameException(userModel.UserName); ;
            }

            CreatePasswordHash(userModel.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            var addedUser = await _userRepository.AddAsync(user, ct);

            return _mapper.Map<UserModel>(addedUser);
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers(CancellationToken ct)
        {

            IEnumerable<User> entities = await _userRepository.ListAllAsync(ct);
            return _mapper.Map<List<UserModel>>(entities);
        }

        public async Task<UserModel> GetById(int id, CancellationToken ct)
        {

            if (id < 1)
                throw new NotFoundException(nameof(UserModel), id);

            var result = await _userRepository.GetByIdAsync(id, ct);

            return _mapper.Map<UserModel>(result);
        }
    }
}
