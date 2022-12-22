global using Microsoft.AspNetCore.Components.Authorization;
global using Blazored.LocalStorage;
using FamilyFinance.UI.Service;
using FamilyFinance.UI.Service.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinanceUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri (builder.Configuration.GetSection("Endpoints:ApiBaseUrl").Value) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IBudgetItemService<BudgetItemModel>, BudgetItemService>();
builder.Services.AddScoped<IFinancialOperationService<FinancialOperationModel>, FinancialOperationService>();
builder.Services.AddScoped<IUserOperationsService<UserOperationModel>, UserOperationService>();
builder.Services.AddScoped<IReportService<ReportModel>, ReportService>();
builder.Services.AddScoped<IUserService<UserModel>, UserService>();
builder.Services.AddScoped<IHttpHandler, HttpHandler>();


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");

app.Run();
