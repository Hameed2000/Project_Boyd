// You can ignore this file, it's just some boilerplate to start up the project

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace ProjectBoyd {

    public class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
