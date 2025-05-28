using SearchServiceAPI.Services; // ⬅️ Bu çok önemli!

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthorization();

// Controller'ları haritalandır
app.MapControllers();

app.Run();
