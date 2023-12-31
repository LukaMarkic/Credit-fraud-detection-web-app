using credit_fraud_detection_app;
using credit_fraud_detection_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Add sessions
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.MapGet("/api/fraud", ([FromQuery] string distanceFromHome, [FromQuery] string distanceFromLastTransaction, [FromQuery] string purchaseRatio, [FromQuery] string repeatRetailer, [FromQuery] string usedChip, [FromQuery] string usedPinNumber, [FromQuery] string onlineOrder) =>
{
    TransactionInfo transaction = new TransactionInfo(distanceFromHome, distanceFromLastTransaction, purchaseRatio, repeatRetailer, usedChip, usedPinNumber, onlineOrder);

    string apiResponse = Utility.GetApiResponse(transaction);
    return apiResponse;
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapRazorPages();

app.Run();



