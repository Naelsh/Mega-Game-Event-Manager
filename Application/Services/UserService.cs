using Application.Authentication;
using Application.Helpers;
using Application.Models.User;
using AutoMapper;
using Domain;
using Persistence;

namespace Application.Services;

public interface IUserService
{
    Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    Task<User> GetById(int id);
    Task Register(RegisterRequest model);
    Task Update(int id, UserUpdateRequest model);
    Task Delete(int id);
}

public class UserService : BaseService, IUserService
{
    private readonly IMapper _mapper;

    public UserService(
        DataContext context,
        IMapper mapper) :base(context)
    {
        _mapper = mapper;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
    {
        var user = await GetUserWithRolesByUserName(model.Username);

        // validate
        if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
            throw new ArgumentException("Username or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        return response;
    }

    public IEnumerable<User> GetAll()
    {
        var result = _context.Users.Where(u => !u.IsDeleted).ToList();
        if (result == null || result.Count == 0)
        {
            throw new NullReferenceException("No users found");
        }
        return _context.Users;
    }

    public async Task<User> GetById(int id)
    {
        var user = await GetUserById(id);
        return user;
    }

    public async Task Register(RegisterRequest model)
    {
        // validate
        if (_context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");

        // map model to new user object
        var user = _mapper.Map<User>(model);

        // hash password
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // save user
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(int id, UserUpdateRequest model)
    {
        var user = await GetUserById(id);

        if (model.Username != user.Username && _context.Users.Any(x => x.Username == model.Username))
            throw new AppException("Username '" + model.Username + "' is already taken");

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

        // copy model to user and save
        _mapper.Map(model, user);

        if (model.ActivityId > 0)
        {
            var activity = await GetActivityById(model.ActivityId);
            user.Activities.Add(activity);
        }

        if (model.RoleId > 0)
        {
            var role = await GetRoleById(model.RoleId);
            user.Roles.Add(role);
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var user = await GetUserById(id);
        user.IsDeleted = true;
        user.FirstName = "Anonymized " + DateTime.UtcNow.ToString();
        user.LastName = "Anonymized " + DateTime.UtcNow.ToString();
        user.Username = "Anonymized " + DateTime.UtcNow.ToString();
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
