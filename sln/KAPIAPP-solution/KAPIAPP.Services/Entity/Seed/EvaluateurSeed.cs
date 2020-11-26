using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KAPIAPP.Services.Entity.Seed
{
    public class EvaluateurSeed : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                 new User
                 {
                     //Id = 1,
                     FirstName = "Emilie",
                     LastName = "ROBERT",
                     Poste = "Responsable boutique"
                 },
                 new User
                 {
                     //Id = 2,
                     FirstName = "Quentin",
                     LastName = "BORDABERRY",
                     Poste = "Responsable logistique"
                 }
             );
        }
    }
}
