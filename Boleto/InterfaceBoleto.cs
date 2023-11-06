namespace LabReserva.Boleto
{
    public interface InterfaceBoleto
    {
        Task<BoletoResponseContent> pagarBoleto(string numeroBoleto, int userId);

    }
}
