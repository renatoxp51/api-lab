namespace LabReserva.Boleto
{
    public class BoletoResponse
    {
        public BoletoResponseContent data { get; set; }
    }

    public class BoletoResponseContent
    {
        public string boleto { get; set; }
        public int user_id { get; set; }
        public string payment_date { get; set; }
        public string status { get; set; }
    }
}
