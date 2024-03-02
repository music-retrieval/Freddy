using Fretty.Endpoints;

// Console.WriteLine(Essentia.getAudio()[50]);
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add configuration for cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins, 
        policy  => { 
            policy.WithOrigins("http://localhost:8080", "http://localhost:3000"); 
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

IEndpoint weatherForecast = new WeatherEndpoint();
IEndpoint chord = new ChordEndpoint();

weatherForecast.MapEndpoint(app);
chord.MapEndpoint(app);

app.UseCors(myAllowSpecificOrigins);
app.Run();
