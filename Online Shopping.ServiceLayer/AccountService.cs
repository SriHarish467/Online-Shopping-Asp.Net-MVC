using Online_Shopping.Repository;
using Online_Shopping.DomainModel;
using Online_Shopping.ViewModel;
using AutoMapper;


namespace Online_Shopping.ServiceLayer
{
    public class AccountService
    {
        AccountRepository accountRepository = new AccountRepository();

        public User Mapping(UserViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<UserViewModel, User>(userViewModel);
            return user;
        }
        public bool ExistingUserSignUp(UserViewModel userViewModel)
        {
            User user = Mapping(userViewModel);
            return accountRepository.ExistingUserSignUp(user);
        }

        public bool ExistingEmailSignUp(UserViewModel userViewModel)
        {
            User user = Mapping(userViewModel);
            return accountRepository.ExistingEmailSignUp(user);
        }

        public void NewUserSignUp(UserViewModel userViewModel)
        {
            User user = Mapping(userViewModel);
            accountRepository.NewUserSignUp(user);
        }

        public bool Login(LoginViewModel loginViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LoginViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<LoginViewModel, User>(loginViewModel);
            return accountRepository.Login(user);
        }

        public UpdateUserProfileViewModel UpdateProfile(string name)
        {
            User user = accountRepository.UpdateProfile(name);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User,UpdateUserProfileViewModel>());
            var mapper = new Mapper(config);
            UpdateUserProfileViewModel updateUserProfileViewModel = mapper.Map<UpdateUserProfileViewModel>(user);
            return updateUserProfileViewModel;
        }

        public void UpdateProfile(UpdateUserProfileViewModel updateUserProfileViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UpdateUserProfileViewModel,User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<User>(updateUserProfileViewModel);
            accountRepository.UpdateProfile(user);
        }
        public UserViewModel ChangePassword(string name)
        {
            User user = accountRepository.UpdateProfile(name);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>());
            var mapper = new Mapper(config);
            UserViewModel userViewModel = mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public void ChangePassword(UserViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<User>(userViewModel);
            accountRepository.UpdateProfile(user);
        }
    }
}
