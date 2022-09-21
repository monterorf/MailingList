using FakeItEasy;
using FluentAssertions;
using MailingList.Controllers;
using MailingList.Data;
using MailingList.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MailingList.Test.Controller
{
    public class UserControllerTests
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _unitOfWork = A.Fake<IUnitOfWork>();

            //SUT
            _userController = new UserController(_unitOfWork);
        }

       

        [Fact]
        public void UserController_Get_ReturnsSuccess()
        {
            //Arrange - What do I need to bring in?
            var users = A.Fake<IEnumerable<User>>();
            A.CallTo(() => _unitOfWork.GetRepository<User>().GetWhereAsync(null,null,null)).Returns(users);
            //Act
            var result = _userController.Get(null, null);
            //Assert
            //result.Should().BeOfType<Task<IEnumerable<User>>>();
            result.Should().NotBeNull();
        }

    }
}
