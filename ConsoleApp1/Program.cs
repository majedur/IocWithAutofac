using Autofac;
using System;

namespace IoCwithAutofac
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Showoutput>().As<IOutput>();
            builder.RegisterType<DateWriter>().As<IWrite>();
            Container = builder.Build();

            WriteDate();
        }

        public static void WriteDate()
        {
            using (var scope=Container.BeginLifetimeScope())
            {
                var wr = scope.Resolve<IWrite>();
                wr.Writer();

            }
        }

        public interface IOutput

        {
            public void Show(string content);
        }

        public class Showoutput : IOutput
        {
            public void Show(string content)
            {
                Console.WriteLine(content);
                Console.ReadLine();
            }
        }

        public interface IWrite
        {
            public void Writer();
        }

        public class DateWriter: IWrite
        {
            private IOutput _output;
            public DateWriter(IOutput output)
            {
                this._output = output;
            }

            public void Writer()
            {
                _output.Show(DateTime.Today.ToShortDateString());
            }
        }

    }
}
