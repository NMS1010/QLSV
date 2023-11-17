
using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QLSV.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable(nameof(File));
            builder.HasKey(o => new { o.Id });
            builder.HasOne(o => o.HocSinh).WithMany(y => y.Files).HasForeignKey(x => x.IdHocSinh).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.GiaoVien).WithMany(y => y.Files).HasForeignKey(x => x.IdGiaoVien).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
