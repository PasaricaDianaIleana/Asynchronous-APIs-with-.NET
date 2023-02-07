using AsynchronousAPI.Data;
using AsynchronousAPI.Dtos;
using AsynchronousAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source = RequestDb.db"));
var app = builder.Build();

app.UseHttpsRedirection();

//Start endpoint

app.MapPost("api/v1/products", async (AppDbContext context, ListingRequests listingRequest) => {
    
    if(listingRequest == null)
      return Results.BadRequest();


    listingRequest.RequestStatus = "ACCEPT";
    listingRequest.EstimatedCompletionTime = "2023-02-08 14:00";

    await context.ListingRequests.AddAsync(listingRequest);
    await context.SaveChangesAsync();


    return Results.Accepted($"api/v1/productstatus/{listingRequest.RequestId}",listingRequest);
});

//Status endpoint

app.MapGet("api/v1/productstatus/{requestId}" ,(AppDbContext context,string requestId) =>{

    var listingRequest = context.ListingRequests.FirstOrDefault(lr=>lr.RequestId == requestId);
    if (listingRequest == null)
        return Results.NotFound();

    ListingStatus listingStatus = new ListingStatus
    {
        RequestStatus = listingRequest.RequestStatus,
        ResourceURL = String.Empty
    };

    if(listingRequest?.RequestStatus?.ToUpper() == "COMPLETED")
    {
        listingStatus.ResourceURL = $"api/v1/products/{Guid.NewGuid().ToString()}";
        //return Results.Ok(listingStatus);

        //redirect to URL
        return Results.Redirect("");
    }

    listingStatus.EstimatedCompletionTime = "2023-02-08 15:00";
    return Results.Ok(listingStatus);

});


//final endpoint
app.MapGet("api/v1/products/{requestId}", (string requestId) =>
 {

     return Results.Ok("Final result");
 });


app.Run();
