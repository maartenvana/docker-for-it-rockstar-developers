using ExampleWebApp.Storage;

using System.Linq;

namespace ExampleWebApp.Business
{
    public class BusinessClass
    {
        private readonly ExampleContext _context;

        public BusinessClass(ExampleContext context)
        {
            _context = context;
        }

        public string GetTheBusiness()
        {
            var example = _context.Examples.AsEnumerable().LastOrDefault();

            return example.Value;
        }
    }
}