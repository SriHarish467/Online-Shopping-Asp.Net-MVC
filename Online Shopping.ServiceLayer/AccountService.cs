using Online_Shopping.Repository;
using Online_Shopping.DomainModel;
using Online_Shopping.ViewModel;
using AutoMapper;


namespace Online_Shopping.ServiceLayer
{
    public class AccountService
    {
        AccountRepository accountRepository = new AccountRepository();

        public bool ExistingUserSignUp(string Username)
        {
            return accountRepository.ExistingUserSignUp(Username);
        }

        public bool ExistingEmailSignUp(string EmailId)
        {
            return accountRepository.ExistingEmailSignUp(EmailId);
        }

        public void NewUserSignUp(UserViewModel userViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<UserViewModel, User>(userViewModel);
            accountRepository.NewUserSignUp(user);
        }

        public bool Login(string Username,string Password)
        {
            return accountRepository.Login(Username, Password);
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

        public NewPasswordViewModel NewPassword(string Email)
        {
            User user = accountRepository.NewPassword(Email);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, NewPasswordViewModel>());
            var mapper = new Mapper(config);
            NewPasswordViewModel newPasswordViewModel = mapper.Map<NewPasswordViewModel>(user);
            return newPasswordViewModel;
        }

        public NewPasswordViewModel Newpassword(string id)
        {
            User user = accountRepository.Newpassword(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, NewPasswordViewModel>());
            var mapper = new Mapper(config);
            NewPasswordViewModel newPasswordViewModel = mapper.Map<NewPasswordViewModel>(user);
            return newPasswordViewModel;
        }

        public void NewPassword(NewPasswordViewModel newPasswordViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NewPasswordViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<User>(newPasswordViewModel);
            accountRepository.UpdateProfile(user);
        }

        public void UpdateUser(string EmailId,string guid)
        {
            accountRepository.UpdateUser(EmailId,guid);
        }
    }
}
