// See https://aka.ms/new-console-template for more information


using DI_Example;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

var serviceProvider = new ServiceCollection()
    .AddScoped<IUserRepo, UserRepo1>()
    .AddScoped<UserService>()
    .BuildServiceProvider();

var userService = serviceProvider.GetService<UserService>();

foreach (var user in userService.GetAllUsers()) { Console.WriteLine(user.Name); }

