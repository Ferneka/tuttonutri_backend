using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Hosting;

namespace TuttoNutri.Infrastructure.Nutrition;

// Carrega o taco.json uma única vez e mantém em memória.
// Registrar como Singleton no Program.cs (do projeto API):
// builder.Services.AddSingleton<TacoDataProvider>();
public class TacoDataProvider
{
    private readonly List<TacoFoodItem> _itens;

    public TacoDataProvider(IHostEnvironment env)
    {
        // O arquivo precisa estar em: TuttoNutri.API/Data/taco.json
        // (ContentRootPath aponta pra raiz do projeto que está rodando, ou seja, a API)
        var path = Path.Combine(env.ContentRootPath, "Data", "taco.json");

        if (!File.Exists(path))
        {
            _itens = new List<TacoFoodItem>();
            return;
        }

        var json = File.ReadAllText(path);
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var raw = JsonSerializer.Deserialize<List<TacoJsonDto>>(json, options) ?? new();

        _itens = raw.Select(r => new TacoFoodItem
        {
            Id = r.Id,
            Descricao = r.Descricao ?? string.Empty,
            Categoria = r.Categoria ?? string.Empty,
            Kcal = r.Kcal,
            ProteinaG = r.ProteinaG,
            GorduraG = r.GorduraG,
            CarboidratoG = r.CarboidratoG,
            FibraG = r.FibraG,
        }).ToList();
    }

    public IReadOnlyList<TacoFoodItem> Todos => _itens;

    private class TacoJsonDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("descricao")] public string? Descricao { get; set; }
        [JsonPropertyName("categoria")] public string? Categoria { get; set; }
        [JsonPropertyName("kcal")] public double? Kcal { get; set; }
        [JsonPropertyName("proteina_g")] public double? ProteinaG { get; set; }
        [JsonPropertyName("gordura_g")] public double? GorduraG { get; set; }
        [JsonPropertyName("carboidrato_g")] public double? CarboidratoG { get; set; }
        [JsonPropertyName("fibra_g")] public double? FibraG { get; set; }
    }
}