using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QLSV.Configurations
{
    public class GiaoVienConfiguration : IEntityTypeConfiguration<GiaoVien>
    {
        public void Configure(EntityTypeBuilder<GiaoVien> builder)
        {
            builder.ToTable(nameof(GiaoVien));
            builder.HasKey(o => o.Id);
            builder.HasMany(o => o.KhoaHocs).WithOne(y => y.GiaoVien).HasForeignKey(x => x.IdGiaoVien).OnDelete(DeleteBehavior.Restrict);
        }
    }
}