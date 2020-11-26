using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAPIAPP.Services.Entity.Seed
{
    public class BoutiqueSeed : IEntityTypeConfiguration<Boutique>
    {
        public void Configure(EntityTypeBuilder<Boutique> builder)
        {
            builder.HasData(
                 new Boutique
                 {
                     Id = 1,
                     Name = "Boutique des Lilas",
                     City = "Bordeaux"                     
                 },
                 new Boutique
                 {
                     Id = 2,
                     Name = "Boutique des Roses",
                     City = "Arcachon"
                 }
             );
        }
    }
}
