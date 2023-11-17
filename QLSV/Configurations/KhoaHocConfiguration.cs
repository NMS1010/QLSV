using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace QLSV.Configurations
{
    public class KhoaHocConfiguration:IEntityTypeConfiguration<KhoaHoc>
    {
        public void Configure(EntityTypeBuilder<KhoaHoc> builder)
        {
            builder.ToTable(nameof(KhoaHoc));
            builder.HasKey(o => o.Id);
        }
    }
}
