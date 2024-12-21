using Cafm.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Cafm.Domain.TaskStatus;

namespace Cafm.Infrastructure.Data
{
    public class CafmContext : DbContext
    {
        public CafmContext()
        {
                
        }
        public CafmContext(DbContextOptions<CafmContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public DbSet<WorkOrder> WorkOrders { get; set; }
        public DbSet<WorkOrderDetail> WorkOrderDetails { get; set; }
        public DbSet<TaskPriority> TaskPriorities { get; set; }
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<Asset> Assets { get; set; }
       // public DbSet<SystemMessage> MessagesSystem { get; set; }
    }
}
