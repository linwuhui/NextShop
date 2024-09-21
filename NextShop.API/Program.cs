
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

            #region 注册数据库上下文服务

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

            #region 允许http://127.0.0.1:3000的任何Http头部的任何方法访问服务器

            app.UseCors(option =>
            {
                option.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://127.0.0.1:3000/");
            });

            #endregion


            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            #region 创建一个范围，再基于此范围注入数据库上下文和日志服务

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
