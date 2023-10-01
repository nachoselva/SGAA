using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGAA.Repository.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using SGAA.Repository.Configuration.Base;
    using SGAA.Utils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class EfMigrationExtensions
    {
        private static string GetDefaultConstraintName(string columnName, string tableName)
        {
            return $"CHK_{tableName}_{columnName}";
        }
        private static string GetDefaultIndexName(string columnName, string tableName)
        {
            return $"IX_{tableName}_{columnName}";
        }

        public static CheckConstraintBuilder HasCheckConstraintWithEnum<TEntity, TProperty>(this TableBuilder<TEntity> tableBuilder, Expression<Func<TEntity, TProperty>> propertyExpression)
            where TEntity : class
            where TProperty : struct, Enum
        {
            IEnumerable<string> optionNames = EnumTools.GetOptions<TProperty>().Select(opt => opt.Name);
            EntityTypeBuilder<TEntity> entityBuilder = (EntityTypeBuilder<TEntity>)((IInfrastructure<EntityTypeBuilder>)tableBuilder).Instance;
            string tableName = tableBuilder.Metadata.GetTableName()!;
            string columnName = entityBuilder.Property(propertyExpression).Metadata.GetColumnName();
            return tableBuilder.HasCheckConstraint(GetDefaultConstraintName(columnName, tableName), $"[{columnName}] IN ({string.Join(", ", optionNames.Select(name => $"'{name}'"))})");
        }

        public static DataBuilder<TEntity> HasDataFromEnum<TEntity, TProperty>(this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, TProperty>> propertyExpression, Expression<Func<EnumOption<TProperty>, TEntity>> mapping)
             where TEntity : class
             where TProperty : struct, Enum
        {
            string tableName = builder.Metadata.GetTableName()!;
            string columnName = builder.Property(propertyExpression).Metadata.GetColumnName();
            builder.HasIndex(propertyExpression.CastExpression<TEntity, TProperty, object?>(), GetDefaultIndexName(columnName, tableName)).IsUnique();
            IEnumerable<TEntity> data = EnumTools.GetOptions<TProperty>().Select(r => mapping.Compile()(r));
            return builder.HasData(data);
        }

        public static PropertyBuilder DecimalColumn(
        this PropertyBuilder propertyBuilder)
        {
            return propertyBuilder
            .HasColumnType(DataTypes.TYPE_DECIMAL)
                .HasPrecision(14, 2);
        }
    }
}
