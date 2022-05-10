using Application.Authentication;
using Application.Helpers;
using Application.Models.User;
using Application.Services;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace API.Controllers;

public class UsersController : BaseController
{
    private IUserService _userService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;

    public UsersController(
        IUserService userService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _userService = userService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        AuthenticateResponse response = null;
        try
        {
            response = _userService.Authenticate(model);
        }
        catch (ArgumentException ae)
        {
            BadRequest(ae.Message);
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }
        return Ok(response);
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        IEnumerable<User> users = null;
        try
        {
            users = _userService.GetAll();
        }
        catch (NullReferenceException nre)
        {
            NotFound(nre.Message);
        }
        catch (Exception ex)
        {
            BadRequest(ex.Message);
        }
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }


    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UserUpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}
