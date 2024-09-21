
using Microsoft.EntityFrameworkCore;

using NextShop.API.Data;

namespace NextShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();

            #region ע�����ݿ������ķ���

            builder.Services.AddDbContext<StoreContext>(option =>
            {
                option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            #region ����http://127.0.0.1:3000���κ�Httpͷ�����κη������ʷ�����

            app.UseCors(option =>
            {
                option.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:3000/");
            });

            #endregion


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            #region ����һ����Χ���ٻ��ڴ˷�Χע�����ݿ������ĺ���־����

            var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            try
            {
                context.Database.Migrate();
                DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                logger.LogError($"An error occured => {ex.Message}");
            }

            #endregion

            app.Run();
        }
    }
}
