namespace Shop.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Shop.Data.Common.Repositories;
    using Shop.Data.Models;
    using Shop.Services.Mapping;

    public class UserProfileService : IUserProfileService
    {
        private readonly IDeletableEntityRepository<UserProfile> userProfileRepository;

        public UserProfileService(IDeletableEntityRepository<UserProfile> userProfileRepository)
        {
            this.userProfileRepository = userProfileRepository;
        }

        public async Task<string> CreateAsync(string firstname, string lastname, string biography, string profilePhoto)
        {
            var userprofile = new UserProfile
            {
                FirstName = firstname,
                LastName = lastname,
                Biography = biography,
                ProfilePhoto = profilePhoto,
            };

            await this.userProfileRepository.AddAsync(userprofile);

            await this.userProfileRepository.SaveChangesAsync();
            return userprofile.Id;
        }

        public T GetById<T>(string id)
        {
            var userprofile = this.userProfileRepository.All().Where(x => x.Id == id)
                 .To<T>().FirstOrDefault();
            return userprofile;
        }
    }
}
