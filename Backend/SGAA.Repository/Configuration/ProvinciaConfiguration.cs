namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class ProvinciaConfiguration : EntityConfiguration<Provincia>
    {
        public override void Configure(EntityTypeBuilder<Provincia> builder)
        {
            base.Configure(builder);
            builder.Property(h => h.Id)
                .ValueGeneratedNever();
            builder.Property(h => h.Nombre)
                .IsRequired();
            builder.Property(h => h.NombreCompleto)
                .IsRequired();

            string url = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\JsonSeeds\\provincias.json";

            IEnumerable<Provincia> provincias = JsonSerializer.Deserialize<ProvinceJson>(File.ReadAllText(url))!
                .Provincias
                .Select(p => new Provincia(int.Parse(p.Id), p.Nombre, p.NombreCompleto));

            builder.HasData(provincias);
        }

        private class ProvinceJson
        {
            [JsonPropertyName("provincias")]
            public IList<ProvinceItemJson> Provincias { get; set; } = default!;
        }

        private class ProvinceItemJson
        {
            [JsonPropertyName("id")]
            public string Id { get; set; } = default!;

            [JsonPropertyName("nombre_completo")]
            public string NombreCompleto { get; set; } = default!;

            [JsonPropertyName("nombre")]
            public string Nombre { get; set; } = default!;
        }
    }
}
