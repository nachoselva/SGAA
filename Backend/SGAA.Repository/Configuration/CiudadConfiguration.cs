namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class CiudadConfiguration : EntityConfiguration<Ciudad>
    {
        public override void Configure(EntityTypeBuilder<Ciudad> builder)
        {
            base.Configure(builder);
            builder.Property(h => h.Id)
                .ValueGeneratedNever();
            builder.Property(h => h.Nombre)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_NAME_LENGTH);
            builder.Property(h => h.NombreCompleto)
                .IsRequired()
                .HasMaxLength(DataTypes.TEXT_LONG_NAME_LENGTH);
            builder.Property(h => h.ProvinciaId)
                .IsRequired();

            builder
                .HasOne(r => r.Provincia)
                .WithMany(mr => mr.Ciudades)
                .HasForeignKey(ur => ur.ProvinciaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            string url = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\JsonSeeds\\ciudades.json";

            IEnumerable<Ciudad> ciudades = JsonSerializer.Deserialize<CityJson>(File.ReadAllText(url))!
            .Municipios
                    .Select(p => new Ciudad(int.Parse(p.Id), int.Parse(p.Provincia.Id), p.Nombre, p.NombreCompleto));
            builder.HasData(ciudades);
        }

        private class CityJson
        {
            [JsonPropertyName("municipios")]
            public IList<CityItemJson> Municipios { get; set; } = default!;
        }

        private class CityItemJson
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = default!;

            [JsonPropertyName("provincia")]
            public CityItemProvinciaJson Provincia { get; set; } = default!;

            [JsonPropertyName("nombre_completo")]
            public string NombreCompleto { get; set; } = default!;

            [JsonPropertyName("nombre")]
            public string Nombre { get; set; } = default!;
        }

        private class CityItemProvinciaJson
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = default!;
        }
    }
}
