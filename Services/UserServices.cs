using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NewMission.Database;
using NewMission.Protos;
using NewMission.Models;
using NewMission.Services;



namespace NewMission.Services
{
    public class UserServices(AppDbContext dbContext , IPrinter printer) : DoService.DoServiceBase
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IPrinter _printer = printer;
        public override async Task<CreateUserReply> CreateUser (CreateUserRequest request , ServerCallContext context)
        {

            if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email || u.Name == request.Name) )
            {
                throw new RpcException(new(StatusCode.InvalidArgument, "This Name or Email is used before, please enter another name : "));
            }
            var NewUser = new User(request.Name,request.Email,request.Password);
            await _dbContext.Users.AddAsync(NewUser);
            await _dbContext.SaveChangesAsync();
            return await Task.FromResult(new CreateUserReply
            {
                Id =  NewUser.UserId,
                Name = request.Name,
            });

        }
        public override async Task<LoginUserReply> UserLogin(LoginUserRequest request, ServerCallContext context)
        {
            var User = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email&& u.Password == request.Password);
            if (User == null) 
            {
                _printer.UserLoginPrinterRequest();
                return null;
            }
            else
            {
                _printer.UserLogoutPrinterReply(User.Name);
                return await Task.FromResult<LoginUserReply>(new LoginUserReply
                {
                    Name=User.Name 
                });
                 
            }
        }
    }
}