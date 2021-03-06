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
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
        AuthenticateResponse response = null;
        try
        {
            response = await _userService.Authenticate(model);
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        User user = null;
        try
        {
            user = await _userService.GetById(id);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        try
        {
            await _userService.Register(model);
        }
        catch (AppException ae)
        {
            return BadRequest(ae.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "Registration successful" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserUpdateRequest model)
    {
        try
        {
            await _userService.Update(id, model);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _userService.Delete(id);
        }
        catch (KeyNotFoundException knfe)
        {
            return NotFound(knfe.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        return Ok(new { message = "User deleted successfully" });
    }
}
