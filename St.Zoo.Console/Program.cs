using Autofac;
using Microsoft.Extensions.FileProviders;
using St.Zoo.Business;
using St.Zoo.Data;
using System.IO;

namespace St.Zoo.Console
{
    class Program
    {
        /// <summary>
        /// The prices file path.
        /// </summary>
        private const string Prices_File_Path = "prices.txt";
        /// <summary>
        /// The animals file path.
        /// </summary>
        private const string Animals_File_Path = "zoo.xml";

        /// <summary>
        /// The species file path.
        /// </summary>
        private const string Species_File_Path = "animals.csv";

        static void Main(string[] args)
        {
            try
            {
                var container = InitializeContainer();

                var service = container.Resolve<IZooService>();
                var amount = service.GetTotalFoodPrice();

                System.Console.WriteLine($"Total Amount: {amount}");
            }
            catch (System.Exception ex)
            {
#if DEBUG
                System.Console.WriteLine(ex.ToString());
#else
                System.Console.WriteLine(ex.Message);
#endif

            }

            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
        }

        /// <summary>
        /// Initialize container
        /// </summary>
        /// <returns></returns>
        public static IContainer InitializeContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ZooService>().As<IZooService>();

            var provider = new PhysicalFileProvider(Directory.GetCurrentDirectory());

            builder.RegisterType<FoodRepository>().As<IFoodRepository>()
             .WithParameter(new TypedParameter(typeof(IFileInfo), provider.GetFileInfo(Prices_File_Path)));
            builder.RegisterType<AnimalSpecieRepository>().As<IAnimalSpecieRepository>()
                 .WithParameter(new TypedParameter(typeof(IFileInfo), provider.GetFileInfo(Species_File_Path)));
            builder.RegisterType<AnimalRepository>().As<IAnimalRepository>()
               .WithParameter(new TypedParameter(typeof(IFileInfo), provider.GetFileInfo(Animals_File_Path)));

            return builder.Build();
        }
    }
}