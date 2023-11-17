
using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QLSV.Configurations
{
    public class DiemHocSinhConfiguration : IEntityTypeConfiguration<DiemHocSinh>
    {
        public void Configure(EntityTypeBuilder<DiemHocSinh> builder)
        {
            builder.ToTable(nameof(DiemHocSinh));
            //builder.HasKey(o => new { o.IdHocSinh,o.IdKhoaHoc });
            builder.HasOne(o => o.HocSinh).WithMany(y => y.DiemHocSinhs).HasForeignKey(x => x.IdHocSinh).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(o => o.KhoaHoc).WithMany(y => y.DiemHocSinhs).HasForeignKey(x => x.IdKhoaHoc).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
