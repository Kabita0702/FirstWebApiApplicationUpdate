using FirstWebApiApplication.Repository;
using System.Net;
using FirstWebApiApplication.Models.DTO;
using FirstWebApiApplication.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApiApplication.Service
{
    public class UserService
    {
        public UserDetailsDTO GetUserById(int id)
        {
            var userDetailsList = UserDataRepository.GetUserDetailsData();
            var userDetails = userDetailsList.FirstOrDefault(x => x.UserId == id);
            if (userDetails == null)
            {
                throw new Exception("Data not found");
            }
            return new UserDetailsDTO() 
            { 
                FirstName=userDetails.FirstName,
                LastName=userDetails.LastName,
                Address=userDetails.Address,
                UserId=userDetails.UserId
            };
        }
        public List<UserDetailsDTO> GetAllUsers()
        {
            var userDetailsList = UserDataRepository.GetUserDetailsData();
            if (userDetailsList.Count == 0)
            {
                throw new Exception("No Data Found");
            }
            return userDetailsList.Select(user
                => new UserDetailsDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address,
                    UserId = user.UserId
                }).ToList();
            
        }
        public Response CreateUser(AddUserDTO inputRequest)
        {
            var userList = UserDataRepository.GetUserData();
            var flag = userList.FirstOrDefault(item => item.UserId == inputRequest.UserId);
            if (flag != null)
            {
                throw new Exception("Already exist id");
            }
            //Random random = new Random();
            var userDetailsList = UserDataRepository.GetUserDetailsData();
            var idVal = userDetailsList.Count > 0 ? userDetailsList[userDetailsList.Count - 1].Id + 1 : 0;
            userList.Add(new User()
            {
                UserId = idVal,
                UserName = inputRequest.UserName,
                Password=inputRequest.Password
            });
            
            userDetailsList.Add(new UserDetails()
            {
                UserId = idVal,
                FirstName = inputRequest.FirstName,
                LastName=inputRequest.LastName,
                Address = inputRequest.Address,
                Email=inputRequest.UserEmail,
                Gender=inputRequest.Gender,
                Specilization=inputRequest.Specilization,
                IsEmployee = inputRequest.IsStudent,
                IsActive = true,
            });
            UserDataRepository.WriteUserData(userList);
            UserDataRepository.WriteUserDetailsData(userDetailsList);
            return new Response() { StatusCode = HttpStatusCode.OK, Message = "Successfully Created" };
        }
        public Response DeleteUser(int id)
        {
            var userDetailsList = UserDataRepository.GetUserDetailsData();
            var flag = userDetailsList.FirstOrDefault(item => item.UserId == id);
            if (flag == null)
            {
                throw new Exception("User Id doesn't exist");
            }
            if (flag.IsActive)
            {
                flag.IsActive = false;
                UserDataRepository.WriteUserDetailsData(userDetailsList);
                return new Response() { StatusCode = HttpStatusCode.OK, Message = "Successfully Deleted" };
            }
            return new Response() { StatusCode = HttpStatusCode.OK, Message = "Already Deleted" };

        }
        public  Response LoginUser(User inputRequest)
        {
            var userList = UserDataRepository.GetUserData();
            var flag = userList.FirstOrDefault(item => (item.UserName == inputRequest.UserName && item.Password==inputRequest.Password));
            if (flag == null) 
            {
                throw new Exception("Invaild user please check the username and password");
            }
            return new Response() 
            { 
                StatusCode = HttpStatusCode.OK,
                Message = " Login Successfully"
            };
        }

        public Response GenericAddUser(AddUserDTO inputRequest)
        {
            var genricUserObj = new GenricClass<User>();
            var userList = genricUserObj.ReadData(@".\Json\User.json");
            var flag = userList.FirstOrDefault(item => item.UserId == inputRequest.UserId);
            if (flag != null)
            {
                throw new Exception("Already exist id");
            }
            
            var genricUserDetailsObj = new GenricClass<UserDetails>();
            var userDetailsList = genricUserDetailsObj.ReadData(@".\Json\UserDetails.json");
            var idVal = userDetailsList.Count > 0 ? userDetailsList[userDetailsList.Count - 1].Id + 1 : 0;
            userList.Add(new User()
            {
                UserId = idVal,
                UserName = inputRequest.UserName,
                Password = inputRequest.Password
            });

            userDetailsList.Add(new UserDetails()
            {
                UserId = idVal,
                FirstName = inputRequest.FirstName,
                LastName = inputRequest.LastName,
                Address = inputRequest.Address,
                Email = inputRequest.UserEmail,
                Gender = inputRequest.Gender,
                Specilization = inputRequest.Specilization,
                IsEmployee = inputRequest.IsStudent,
                IsActive = true,
            });
            genricUserObj.WriteData(@".\Json\User.json", userList);
            genricUserDetailsObj.WriteData(@".\Json\UserDetails.json", userDetailsList);
            
            return new Response() 
            { 
                StatusCode = HttpStatusCode.OK, 
                Message = "Successfully Created" 
            };
        }
    }

}
