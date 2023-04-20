using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using NeZoviReg.Domain;

namespace NeZoviReg.Persistence.Ef.Configuration;

public static class ConfigurationExtension
{
    public static void ConfigureEntity<T>(this EntityTypeBuilder<T> configuration, bool hasDefaultId = true, bool configConcurrent = true) where T : Entity
    {
        if (hasDefaultId)
        {
            configuration.HasKey(ct => ct.Id);
            /*configuration.Property(i => i.Id)
                .ValueGeneratedOnAdd();*/
        }
        else
        {
            configuration.Ignore(i => i.Id);
        }

        configuration.ConfigureAuditable();

        if (configConcurrent)
        {
            configuration.Property(i => i.Rowversion)
                .HasColumnType("timestamp")
                .HasMaxLength(8)
                .IsRowVersion();
        }
        else
        {
            configuration.Ignore(i => i.Rowversion);
        }
    }

    public static void ConfigureEnumerationEntity<T>(this EntityTypeBuilder<T> configuration, bool hasDefaultId = true, bool configConcurrent = true)
        where T : EnumerationEntity<T>
    {
        if (hasDefaultId)
        {
            configuration.HasKey(ct => ct.Id);
            /*configuration.Property(i => i.Id)
                .ValueGeneratedOnAdd();*/
        }
        else
        {
            configuration.Ignore(i => i.Id);
        }

        configuration.ConfigureAuditable();

        if (configConcurrent)
        {
            configuration.Property(i => i.Rowversion)
                .HasColumnType("timestamp")
                .HasMaxLength(8)
                .IsRowVersion();
        }
        else
        {
            configuration.Ignore(i => i.Rowversion);
        }
    }

    public static EntityTypeBuilder<T> ConfigureAuditable<T>(this EntityTypeBuilder<T> builder)
        where T : class, IAuditableEntity
    {
        builder.Property(i => i.CreatedBy)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.CreatedOn)
            .HasColumnType("datetime2");

        builder.Property(i => i.ModifiedBy)
            .HasMaxLength(100);

        builder.Property(i => i.ModifiedOn)
            .HasColumnType("datetime2");

        return builder;
    }

}