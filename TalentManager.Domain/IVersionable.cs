namespace TalentManager.Domain
{
    public interface IVersionable
    {
        byte[] RowVersion { get; set; }
    }
}
