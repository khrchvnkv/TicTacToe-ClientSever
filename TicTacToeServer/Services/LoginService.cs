using TicTacToe.Core.IConfiguration;
using TicTacToe.Models;

namespace TicTacToe.Services
{
    public sealed class LoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> CreateUserOrLogin(string username)
        {
            if (username is null || string.IsNullOrEmpty(username))
                throw new ArgumentNullException($"{nameof(username)} is null or empty");

            var createdUser = await _unitOfWork.Users.GetByName(username);
            if (createdUser is null)
            {
                var newUser = new User()
                {
                    Name = username
                };
            
                await _unitOfWork.Users.Add(newUser);
                await _unitOfWork.CompleteAsync();
                return newUser;
            }

            return createdUser;
        }
    }
}