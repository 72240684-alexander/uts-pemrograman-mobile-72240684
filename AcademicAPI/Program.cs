
using AcademicApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Mengaktifkan controller untuk API Dosen dan Mahasiswa.
builder.Services.AddControllers();

// Menghubungkan Entity Framework Core dengan database SQLite.
builder.Services.AddDbContext<AcademicDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Mengaktifkan dokumentasi API dan Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Mengaktifkan Swagger saat aplikasi dijalankan.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mengaktifkan HTTPS.
app.UseHttpsRedirection();

// Menghubungkan endpoint controller ke aplikasi.
app.MapControllers();

app.Run();
