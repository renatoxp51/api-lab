using Microsoft.EntityFrameworkCore;

namespace LabReserva.Data
{
    public class ReservaLabContext : DbContext 
    {
        // construtor
        public ReservaLabContext(DbContextOptions<ReservaLabContext> options) : base(options) { }


    }
}
