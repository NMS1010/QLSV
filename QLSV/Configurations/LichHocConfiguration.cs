
using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QLSV.Configurations
{
    public class LichHocConfiguration : IEntityTypeConfiguration<LichHoc>
    {
        public void Configure(EntityTypeBuilder<LichHoc> builder)
        {
            builder.ToTable(nameof(LichHoc));
            builder.HasKey(o => new { o.Id });
            builder.HasOne(o => o.HocSinh).WithMany(y => y.LichHocs).HasForeignKey(x => x.IdHocSinh).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.GiaoVien).WithMany(y => y.LichHocs).HasForeignKey(x => x.IdGiaoVien).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
