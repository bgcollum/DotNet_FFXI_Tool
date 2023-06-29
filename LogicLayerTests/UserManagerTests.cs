using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using System.Collections.Generic;
using DataObjects;
using LogicLayer;
using DataAccessLayerInterfaces;
using DataAccessLayer;

using DataAccessLayerFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        /* 
            Red Green Refactor
        */
        UserManager userManager = null;


        [TestInitialize]
        public void TestSetup()
        {
            //userManager = new UserManager();    // Connecting to DB
            userManager = new UserManager(new UserAccessorFakes());
        }

        [TestMethod]
        public void TestUserLogsInWithCorrectUserNameAndPassword()
        {
            // Testing LoginUser(string userEmail, string userPassword)
            // Arrange
            const string userEmail = "anonymous@no.email";
            const string userPassword = "newuser";
            int expectedUserID = 100000;    // Anonymous_User userID (No harm in exposing this user)
            int actualUserID = -1;
            
            // Act
            User testUser = userManager.LoginUser(userEmail, userPassword);
            actualUserID = testUser.UserID;
            // Assert
            Assert.AreEqual(expectedUserID, actualUserID);
            Assert.AreEqual(userEmail, testUser.UserEmail);
            // Test checks out, Anonymous_User can log in
        }
        [TestMethod]
        public void TestSelectAllUserRoles()
        {
            List<string> localList = new List<string>();
            localList.Add("Administrator");
            localList.Add("Anonymous_User");
            localList.Add("Contributor");
            localList.Add("Registered_User");

            List<string> fakeList = userManager.RetrieveUserRoles();

            Assert.AreEqual(localList[0], fakeList[0]);
            Assert.AreEqual(localList[1], fakeList[1]);
            Assert.AreEqual(localList[2], fakeList[2]);
            Assert.AreEqual(localList[3], fakeList[3]);
        }
    }
}
