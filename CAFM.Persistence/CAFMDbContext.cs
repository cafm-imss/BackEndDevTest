using CAFM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAFM.Persistence
{
    public class CAFMDbContext : DbContext
    {
        public CAFMDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Asset> Assets { get; set; }

        public virtual DbSet<ErrorsLog> ErrorsLogs { get; set; }

        public virtual DbSet<MessagesSystem> MessagesSystems { get; set; }

        public virtual DbSet<TaskPriority> TaskPriorities { get; set; }

        public virtual DbSet<TaskStatue> TaskStatues { get; set; }

        public virtual DbSet<WorkOrder> WorkOrders { get; set; }

        public virtual DbSet<WorkOrderDetail> WorkOrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.AssetName)
                    .IsRequired()
                    .HasMaxLength(1000);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.ImagePath).HasMaxLength(500);
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.WeeklyOperationHours).HasDefaultValue((byte)50);
            });

            builder.Entity<ErrorsLog>(entity =>
            {
                entity.ToTable("ErrorsLog");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
                entity.Property(e => e.ErrMsg)
                    .IsRequired()
                    .HasMaxLength(250);
                entity.Property(e => e.ErrSource)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ErrTime)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("smalldatetime");
                entity.Property(e => e.LocalHost)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Notes).HasMaxLength(500);
                entity.Property(e => e.SendByEmail).HasColumnType("smalldatetime");
                entity.Property(e => e.Serial)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(15);
            });

            builder.Entity<MessagesSystem>(entity =>
            {
                entity.HasKey(e => new { e.MsgId, e.Lang }).HasName("PK_MESSAGES_SYSTEM");

                entity.ToTable("Messages_System");

                entity.Property(e => e.MsgId).HasColumnName("MsgID");
                entity.Property(e => e.Lang)
                    .HasMaxLength(3)
                    .IsUnicode(false);
                entity.Property(e => e.MsgText)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            builder.Entity<TaskPriority>(entity =>
            {
                entity.HasKey(e => e.PriorityId).HasName("PK_TASK_PRIORITIES");

                entity.Property(e => e.PriorityColor)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.PriorityName)
                    .IsRequired()
                    .HasMaxLength(400);
                entity.Property(e => e.PriorityNameEn).HasMaxLength(400);
            });

            builder.Entity<TaskStatue>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(500);
                entity.Property(e => e.StatusNameEn).HasMaxLength(500);
            });

            builder.Entity<WorkOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CompletionDate).HasColumnType("datetime");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.DueDate).HasColumnType("datetime");
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Asset).WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.AssetId)
                    .HasConstraintName("FK_WorkOrders_Assets");

                entity.HasOne(d => d.Priority).WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.PriorityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_TaskPriorities");

                entity.HasOne(d => d.TaskStatus).WithMany(p => p.WorkOrders)
                    .HasForeignKey(d => d.TaskStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrders_TaskStatues");
            });

            builder.Entity<WorkOrderDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.FileUrl).HasColumnName("FileURL");
                entity.Property(e => e.ImgUrl).HasColumnName("ImgURL");
                entity.Property(e => e.IsDeleted).IsRequired();
                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
                entity.Property(e => e.VoiceUrl).HasColumnName("VoiceURL");

                entity.HasOne(d => d.WorkOrder).WithMany(p => p.WorkOrderDetails)
                    .HasForeignKey(d => d.WorkOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_WorkOrderDetails_WorkOrders");
            });

            base.OnModelCreating(builder);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
