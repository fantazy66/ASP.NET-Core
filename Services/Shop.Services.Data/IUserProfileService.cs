namespace Shop.Services.Data
{
    using System.Threading.Tasks;

    public interface IUserProfileService
    {
        Task<string> CreateAsync(string firstname, string lastname, string biography, string profilePhoto);

        T GetById<T>(string id);
    }
}
