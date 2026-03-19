namespace EventPlusTorloni.WebAPI.DTO;

public class PresencaDTO
{
    public Guid? Id { get; set; }
    public bool Situacao { get; set; }
    public Guid? IdEvento { get; set;}
    public Guid? IdUsuario { get; set; }
}