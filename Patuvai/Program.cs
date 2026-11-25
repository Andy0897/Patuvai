using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Patuvai.Data;
using Patuvai.Hubs;
using Patuvai.Repositories;
using Patuvai.Repositories.Interfaces;
using Patuvai.Services;
using Patuvai.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IPolicyRepository, PolicyRepository>();

builder.Services.AddScoped<IPolicyService, PolicyService>();

builder.Services.AddScoped<IUserPolicyAcceptanceRepository, UserPolicyAcceptanceRepository>();

builder.Services.AddScoped<IUserPolicyAcceptanceService, UserPolicyAcceptanceService>();

builder.Services.AddScoped<IDriverRepository, DriverRepository>();

builder.Services.AddScoped<IDriverService, DriverService>();

builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<ICityRepository, CityRepository>();

builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddScoped<IListingRepository, ListingRepository>();

builder.Services.AddScoped<IRequestListingRepository, RequestListingRepository>();

builder.Services.AddScoped<IRequestListingService, RequestListingService>();

builder.Services.AddScoped<IConversationRepository, ConversationRepository>();

builder.Services.AddScoped<IConversationService, ConversationService>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();

builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<PasswordService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Home/Index";
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddSignalR()
    .AddJsonProtocol(options =>
    {
        options.PayloadSerializerOptions.PropertyNamingPolicy = null;
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await DbInitializer.SeedAdminAsync(context);
    await DbInitializer.SeedPoliciesAsync(context);
    await DbInitializer.SeedCitiesAsync(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHub<ConversationHub>("/conversationHub");

app.Run();
