using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Remind.Core.Models;
using Remind.Core.Repositories;
using Remind.Core.Services;
using Remind.Data;
using Remind.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<SubjectDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString,
        ServerVersion.AutoDetect(connectionString), x
            => x.MigrationsAssembly("Remind.Data"));
});

// Add services to the container.
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<IObservationService, ObservationService>();
builder.Services.AddAutoMapper(typeof(Program));


// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<SubjectDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication  
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Adding Jwt Bearer  
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
            ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
        };
    });


var app = builder.Build();
app.UseCors(policyBuilder   => policyBuilder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("content-disposition"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// todo: https://www.c-sharpcorner.com/article/get-started-with-entity-framework-core-using-sqlite/
// todo: https://medium.com/swlh/building-a-nice-multi-layer-net-core-3-api-c68a9ef16368#id_token=eyJhbGciOiJSUzI1NiIsImtpZCI6ImMzYWZlN2E5YmRhNDZiYWU2ZWY5N2U0NmM5NWNkYTQ4OTEyZTU5NzkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiIyMTYyOTYwMzU4MzQtazFrNnFlMDYwczJ0cDJhMmphbTRsamRjbXMwMHN0dGcuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMDE2NTI0MzI5NTQ1OTEwNjc2MjUiLCJlbWFpbCI6ImZyZWV3aWxsaWUyNjVAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsIm5iZiI6MTY5MzI1MDg1MCwibmFtZSI6IkZyZWUgV2lsbGllIiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hL0FBY0hUdGVydUVuQ1NhbVBOUjJlNjJUT09iWUpvMmc4bXVWdkRuQkRjNGdSV3UtXz1zOTYtYyIsImdpdmVuX25hbWUiOiJGcmVlIiwiZmFtaWx5X25hbWUiOiJXaWxsaWUiLCJsb2NhbGUiOiJlbi1HQiIsImlhdCI6MTY5MzI1MTE1MCwiZXhwIjoxNjkzMjU0NzUwLCJqdGkiOiIyZDFkMmQzNDBiMWVjZTY0MWNkZTFiZjAzNWExMTJlMDhjMTc0ZWE0In0.cldv83BczPRuz7-kZA59E63y1X2kCBl49yqc13-0FWwggaBQYQO5_2jB5ydAHj5JSrt5508B83yw5dfsy-Ysns2gO7aipW_PIQ3UkkeHr6PQtbx4P66wsRo0iJm9iftL1gQKAr7PwXqbQSvYRcOjbmxp__GJLAMJZGewN_7Ogvo3wTr20UFhzC3ZgKjBl_TK6IG7p_tGM_prKiwv31snLkdRs0wr63BZRltRAMvHzo3u5-VEh5PAuY3_F6aXdOMP0gVj_gpo76JTzdeLebNUmorSAzbSWoGXDjOa-hoXM1_ADr7INWgOsBC2v-nF6JZvMiCeNpLk4XRl6H8S4b2crw
// todo: https://shahedbd.medium.com/net-7-web-api-jwt-authentication-and-role-based-authorization-f2ff81f69cd4