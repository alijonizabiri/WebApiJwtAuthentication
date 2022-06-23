using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Models;
using WebApi.Services.Account;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController:ControllerBase
{
    private readonly AccountService _accountService;
    private readonly UserService _userService;

    public AccountController(AccountService accountService, UserService userService)
    {
        _accountService = accountService;
        _userService = userService;
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (ModelState.IsValid == false) return BadRequest();
        return Ok(await _accountService.Login(loginDto));
    }
    
    //register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (ModelState.IsValid == false) return BadRequest(registerDto);
        var result = await _accountService.Register(registerDto);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
    
    //get user list
    [HttpGet("getUserList")]
    public async Task<IActionResult> GetUserList()
    {
        return Ok(await _userService.GetUsers());
    }
    
    [HttpGet("InsertUser")]
    public async Task<IActionResult> InsertUser(UserViewModel userViewModel)
    {
        return Ok(await _userService.InsertUser(userViewModel));
    }
}