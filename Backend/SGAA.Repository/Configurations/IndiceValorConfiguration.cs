namespace SGAA.Repository.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using SGAA.Domain.Core;
    using SGAA.Repository.Configuration.Base;
    using System.Reflection;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class IndiceValorConfiguration : EntityConfiguration<IndiceValor>
    {
        public override void Configure(EntityTypeBuilder<IndiceValor> builder)
        {
            base.Configure(builder);

            builder.Property(indicevalor => indicevalor.FechaDesde)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DATE);

            builder.Property(indicevalor => indicevalor.Valor)
                .IsRequired()
                .HasColumnType(DataTypes.TYPE_DECIMAL);

            builder
                .HasOne(valor => valor.Indice)
                .WithMany(indice => indice.Valores)
                .HasPrincipalKey(indice => indice.Id)
                .HasForeignKey(valor => valor.IndiceId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            string url = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "//JsonSeeds//icl.json";

            IEnumerable<IndiceValor> valores = JsonSerializer.Deserialize<IndiceJson>(File.ReadAllText(url))!
                .Valores
                .Select(p => new IndiceValor(1, p.Id, p.Fecha, p.Valor));

            builder.HasData(valores);

            builder.ToTable(nameof(IndiceValor));
        }
        private class IndiceJson
        {
            [JsonPropertyName("valores")]
            public IList<IndiceValorJson> Valores { get; set; } = default!;
        }

        private class IndiceValorJson
        {
            [JsonPropertyName("id")]
            public int Id { get; set; } = default!;

            [JsonPropertyName("fecha")]
            public DateOnly Fecha { get; set; } = default!;

            [JsonPropertyName("valor")]
            public decimal Valor { get; set; } = default!;
        }
    }
}
