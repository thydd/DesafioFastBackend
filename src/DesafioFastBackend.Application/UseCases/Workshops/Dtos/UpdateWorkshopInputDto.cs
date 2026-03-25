using System.Text.Json.Serialization;

namespace DesafioFastBackend.Application.UseCases.Workshops.Dtos;

public sealed record UpdateWorkshopInputDto
{
    private DateTime dataRealizacao;

    public int Id { get; init; }
    public string Nome { get; init; } = string.Empty;
    public string Descricao { get; init; } = string.Empty;

    public DateTime DataRealizacao
    {
        get => dataRealizacao;
        init => dataRealizacao = value;
    }

    [JsonPropertyName("dataHora")]
    public DateTime? DataHora
    {
        init
        {
            if (value.HasValue)
            {
                dataRealizacao = value.Value;
            }
        }
    }

    [JsonPropertyName("dataHoraRealizacao")]
    public DateTime? DataHoraRealizacao
    {
        init
        {
            if (value.HasValue)
            {
                dataRealizacao = value.Value;
            }
        }
    }
}
