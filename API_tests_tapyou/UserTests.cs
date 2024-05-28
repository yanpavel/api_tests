using NUnit.Framework;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace API_tests_tapyou
{
    [TestFixture]
    public class UserTests
    {
        string _baseUrl = "https://hr-challenge.dev.tapyou.com/api/test/";
        List<long>? _maleUsers;
        List<long>? _femaleUsers;

        [Test, Order(1), Description("Отправить запрос с gender = male")]
        public async Task UserTest1()
        {
            var client = new RestClient(_baseUrl);
            var args = new
            {
                gender = "male"
            };
            var response = await client.GetJsonAsync<JsonRoot>("users", args);
            _maleUsers = response.IdList.ToList(); //save Id values of male users
            Assert.That(response.IsSuccess, Is.EqualTo(true)); //checking is request successful or not            
        }

        [Test, Order(2), Description("Отправить запрос с gender = female")]
        public async Task UserTest2()
        {
            var client = new RestClient(_baseUrl);
            var args = new
            {
                gender = "female"
            };
            var response = await client.GetJsonAsync<JsonRoot>("users", args);
            _femaleUsers = response.IdList.ToList(); //save Id values of male users
            Assert.That(response.IsSuccess, Is.EqualTo(true)); //checking is request successful or not            
        }

        [Test, Description("Сравнить пользователей с gender = female и gender = male")]
        public void UserTest3()
        {
            bool whatuserContain = true;
            List<long> matchUsers = new List<long>();
            for (int i = 0; i < _femaleUsers.Count(); i++)
            {
                if (_maleUsers.Contains(_femaleUsers[i])) //if elements in both lists match then return false and add show these elements
                {
                    whatuserContain = false;
                    matchUsers.Add(_femaleUsers[i]);
                }
            }
            if (!whatuserContain)
            {
                Console.WriteLine("matching user Ids in both of lists:" + string.Join(", ", matchUsers));
            }
            Assert.That(whatuserContain, Is.EqualTo(true)); //checking is request successful or not

        }

        [Test, Description("Проверить данные пользователей из ответа на запрос GET baseUrl{users?gender=male}")]
        public async Task UserTest4()

        {
            Dictionary<long, object> userGender = new Dictionary<long, object>();
            foreach (long c in _maleUsers)
            {
                var client = new RestClient(_baseUrl);
                var response = await client.GetJsonAsync<JsonRoot>($"user/{c}");
                bool date = DateTime.TryParse(response.User.RegistrationDate, out DateTime result);
                DateTime millenium = new DateTime(2000, 1, 1); ;
                if (response.User.Gender != "male" || response.User.Age > 120 || date == false || result < millenium || response.User.Name == null || response.User.City == null) //check user data
                {
                    userGender.Add(c, response.User);
                }
            }
            if (userGender.Count != 0)
            {
                Console.WriteLine("Users with invalid values:");
                foreach (var g in userGender)
                {
                    Console.WriteLine($"RequestMaleUserId:{g.Key}, UserId:{g.Value}"); //Show users with invalid data
                }
            }
            Assert.That(userGender.Count, Is.EqualTo(0));
        }

        [Test, Description("Проверить данные пользователей из ответа на запрос GET baseUrl{users?gender=female}")]
        public async Task UserTest5()
        {
            Dictionary<long, object> userGender = new Dictionary<long, object>();
            foreach (long c in _femaleUsers)
            {
                var client = new RestClient(_baseUrl);
                var response = await client.GetJsonAsync<JsonRoot>($"user/{c}");
                bool date = DateTime.TryParse(response.User.RegistrationDate, out DateTime result);
                DateTime millenium = new DateTime(2000, 1, 1); ;
                if (response.User.Gender != "female" || response.User.Age > 120 || date == false || result < millenium || response.User.Name == null || response.User.City == null) //check user data
                {
                    userGender.Add(c, response.User);
                }
            }
            if (userGender.Count != 0)
            {
                Console.WriteLine("Users with invalid values:");
                foreach (var g in userGender)
                {
                    Console.WriteLine($"RequestFemaleUserId:{g.Key}, UserId:{g.Value}"); //Show users with invalid data
                }
            }
            Assert.That(userGender.Count, Is.EqualTo(0));
        }

        [Test, Description("Отправить запрос с невалидным гендером GET baseUrl{users?gender=queer}")]
        public async Task UserTest6()
        {
            var client = new RestClient(_baseUrl);
            var args = new
            {
                gender = "queer"
            };
            var request = new RestRequest($"users/{args}");
            var response = await client.GetAsync(request);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("NotFound")); //checking is request returning valid status code or not              
        }

        [Test, Description("Отправить запрос с невалидным гендером GET baseUrl{users?gender=1}")]
        public async Task UserTest7()
        {
            var client = new RestClient(_baseUrl);
            var args = new
            {
                gender = 1
            };
            var request = new RestRequest($"users/{args}");
            var response = await client.GetAsync(request);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("NotFound")); //checking is request returning valid status code or not     

        }

        [Test, Description("Отправить запрос с несуществующим пользователем")]
        public async Task UserTest8()
        {
            var client = new RestClient(_baseUrl);
            var request = new RestRequest("users/2048");
            var response = await client.GetAsync(request);
            Assert.That(response.StatusCode.ToString(), Is.EqualTo("NotFound")); //checking is request returning valid status code or not   
        }
    }
}
