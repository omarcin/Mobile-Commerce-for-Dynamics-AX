using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace mAxCommerce.WCF.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(MaxCommerceService));
            try
            {
                serviceHost.Open();
                PrintServiceInfo(serviceHost);
                Console.WriteLine("Press ENTER to close");
                Console.ReadLine();
                serviceHost.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                serviceHost.Abort();
            }
        }

        private static void PrintServiceInfo(ServiceHost serviceHost)
        {
            Console.WriteLine("Service {0} is running with endpoints:", serviceHost.Description.ServiceType);
            foreach (ServiceEndpoint endpoint in serviceHost.Description.Endpoints)
            {
                Console.WriteLine(endpoint.Address);
            }
        }
    }
}
