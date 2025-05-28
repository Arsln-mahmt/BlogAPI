using SearchServiceAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// 🔐 CORS politikası tanımı
var MyAllowAllOrigins = "_myAllowAllOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrigins,
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Servisleri ekle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔌 Arama servisini DI konteynerine kaydet
builder.Services.AddScoped<SearchServiceAPI.Services.ISearchService, SearchServiceAPI.Services.SearchService>();


var app = builder.Build();

// Geliştirme ortamıysa Swagger'ı aç
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 🧩 CORS middleware aktif hale getirildi
app.UseCors(MyAllowAllOrigins);

app.UseAuthorization();

// Controller'ları haritalandır
app.MapControllers();

app.Run();
