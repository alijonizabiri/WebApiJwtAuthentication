using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services.Account;

public class UserService
{
    private readonly UserManager<User> _userManager;
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public UserService(UserManager<User> userManager, IHostEnvironment hostEnvironment, IMapper mapper, DataContext context)
    {
        _userManager = userManager;
        _hostEnvironment = hostEnvironment;
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<User> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
    
    //get user list
    public async Task<List<UserDto>> GetUsers()
    {
        return await _userManager.Users.Select(user=> new UserDto()
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.UserName
        }).ToListAsync();
    }
    
    //get user by id
    public async Task<User> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<int> InsertUser(UserViewModel userView)
    {
        var fileName = Guid.NewGuid()+"_"+ userView.Image.FileName;
        var path = Path.Combine(_hostEnvironment.ContentRootPath, "Images", fileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            await userView.Image.CopyToAsync(stream);
        }

        var map = _mapper.Map<User>(userView);
        map.Image = fileName;
        var add = await _context.Users.AddAsync(map);
        return await _context.SaveChangesAsync();
    }
}