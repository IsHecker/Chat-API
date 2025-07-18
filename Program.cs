using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Chat_API.Data;
using Chat_API.Data.Interfaces;
using Chat_API.Data.Repositories;
using Chat_API.Hubs;
using Chat_API.Hubs.Configs;
using Chat_API.Models;
using Chat_API.Services;
using Chat_API.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            builder.Services.AddControllers();
            builder.Services.AddSignalR().AddJsonProtocol(opts =>
                {
                    opts.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddDbContext<ApplicationDbContext>(opts =>
              opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));


            builder.Services.AddIdentity<User, IdentityRole<Guid>>(opts =>
                {
                    opts.Password.RequiredLength = 4;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireUppercase = false;
                    opts.User.RequireUniqueEmail = true;
                    opts.SignIn.RequireConfirmedEmail = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer((opts) =>
                {
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))
                    };

                    // 👇 THIS tells SignalR to look for access_token in the query string
                    opts.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            // Check if the request is for your SignalR hub and the token exists in the query
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/notifications"))) // <-- Crucial line
                            {
                                context.Token = accessToken; // Set the token for authentication to process
                            }
                            return Task.CompletedTask;
                        }
                    };
                });


            builder.Services.AddAuthorization();

            builder.Services.Configure<JsonOptions>(opts =>
                opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);



            builder.Services.AddSingleton<TokenGeneratorService>();
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            builder.Services.AddScoped<FriendRequestRepository>();
            builder.Services.AddScoped<FriendshipRepository>();

            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<FriendRequestService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<IUnitOfWork>(p => p.GetRequiredService<ApplicationDbContext>());
        }

        var app = builder.Build();
        {
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapHub<NotificationHub>("notifications");

            app.MapControllers();
        }

        app.Run();
    }
}