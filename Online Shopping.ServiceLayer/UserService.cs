using Online_Shopping.Repository;
using Online_Shopping.DomainModel;
using System.Collections.Generic;
using Online_Shopping.ViewModel;
using AutoMapper;

namespace Online_Shopping.ServiceLayer
{
    public class UserService
    {
        UserRepository userRepository = new UserRepository();

        public IEnumerable<UserViewModel> DisplayUser()
        { 
            IEnumerable<User>  user = userRepository.DisplayUser();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>());
            var mapper = new Mapper(config);
            IEnumerable<UserViewModel> userViewModel = mapper.Map<IEnumerable<UserViewModel>>(user);
            return userViewModel;
        }

        public IEnumerable<RoleViewModel> DisplayRole()
        {
            IEnumerable<Role> role = userRepository.DisplayRole();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Role, RoleViewModel>());
            var mapper = new Mapper(config);
            IEnumerable<RoleViewModel> roleViewModel = mapper.Map<IEnumerable<RoleViewModel>>(role);
            return roleViewModel;
        }

        public EditUserViewModel EditUser(int id)
        {
            User user = userRepository.EditUser(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, EditUserViewModel>());
            var mapper = new Mapper(config);
            EditUserViewModel editUserViewModel = mapper.Map<EditUserViewModel>(user);
            return editUserViewModel;
        }

        public void EditUser(EditUserViewModel editUserViewModel)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<EditUserViewModel, User>());
            var mapper = new Mapper(config);
            User user = mapper.Map<User>(editUserViewModel);
            userRepository.EditUser(user);
        }

        public UserViewModel DeleteUser(int id)
        {
            User user = userRepository.DeleteUser(id);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>());
            var mapper = new Mapper(config);
            UserViewModel userViewModel = mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        public void DeleteConfirmed(int id)
        {
            userRepository.DeleteConfirmed(id);
        }
    }
}
