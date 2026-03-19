namespace EventPlusTorloni.WebAPI.DTO
{
    public class ComentarioEventoDTO
    {
        public string? Descricao { get; set; }
        public bool? Exibe { get; set; }
        public DateTime DataComentrioEvento { get; set; }
        public Guid? IdUsuario { get; set; }
        public Guid? IdEvento { get; set; }
    }
}
