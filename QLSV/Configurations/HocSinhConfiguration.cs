using QLSV.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QLSV.Configurations
{
    public class HocSinhConfiguration : IEntityTypeConfiguration<HocSinh>
    {
        public void Configure(EntityTypeBuilder<HocSinh> builder)
        {
            builder.ToTable(nameof(HocSinh));
            builder.HasKey(o => new { o.Id});
            
        }
    }
}
