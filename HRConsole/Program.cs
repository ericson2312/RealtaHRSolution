using Microsoft.Extensions.Configuration;
using System;
using VBHRDBLib;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        private static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {
            BuildConfig();

            IHRVbApi _hrVbApi = new HRVbApi(Configuration.GetConnectionString("HRDS"));
            //Finda ALl Job Role

            var listJobRole = _hrVbApi.RepositoryManager.JobRole.FindAllJobRole();

            foreach (var item in listJobRole)
            {
                Console.WriteLine($"{item}");
            }


            //Update Job Role By SP
            var updateJobRoleBySp = _hrVbApi.RepositoryManager.JobRole.UpdateJobRoleBySp(15, "Bootcamp CodeId", DateTime.Now, true);
            var jobRoleUpdateBySpResult = _hrVbApi.RepositoryManager.JobRole.FindJobRoleById(15);
            Console.WriteLine($"{jobRoleUpdateBySpResult}");


            Console.WriteLine($"_________________________________________________________");
            //Finda ALl Job Role

            var listJobRole1 = _hrVbApi.RepositoryManager.JobRole.FindAllJobRole();

            foreach (var item in listJobRole1)
            {
                Console.WriteLine($"{item}");
            }


        }

        static void MenuJobRole()
        {
            IHRVbApi _hrVbApi = new HRVbApi(Configuration.GetConnectionString("HRDS"));

            //Create Job Role
            var newJobRole = _hrVbApi.RepositoryManager.JobRole.CreateJobRole(new VBHRDBLib.Model.JobRole
            {
                JoroId = 15,
                JoroName = "Planning",
                JoroModifiedDate = DateTime.Now,
            });
            Console.WriteLine($"New Job Role : {newJobRole}");

            //Delete Job Role
            var deletedJobRole = _hrVbApi.RepositoryManager.JobRole.DeleteJobRole(15);
            Console.WriteLine($"Delete Job Role row : {deletedJobRole}");


        }

        static void BuildConfig()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();

            Console.WriteLine(Configuration.GetConnectionString("HRDS"));
        }
    }
}