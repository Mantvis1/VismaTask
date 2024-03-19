using EFCoreInMemoryDbDemo;

namespace VismaTask.Models
{
    public class DataSeeding
    {
        private readonly ApiContext _context;

        public DataSeeding(ApiContext context)
        {
            _context = context;
        }

     
    }
}
