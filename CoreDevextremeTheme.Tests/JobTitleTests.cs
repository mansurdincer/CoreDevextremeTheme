namespace WorkSpace.Tests
{
    [TestClass]
    public class JobTitleTests
    {
        [TestMethod]
        public void CanInsertJobTitle()
        {
            var jobTitle = new JobTitle
            {
                Id = Guid.Parse("a2a2a2a2-a2a2-a2a2-a2a2-a2a2a2a2a2a2"),
                Name = "Manager",
                Description = "Manager",
            };
            Assert.AreEqual("Manager", jobTitle.Name);
        }

        [TestMethod]
        public void CanChangeJobTitleName()
        {
            // Arrange
            var jobTitle = new JobTitle { Name = "Developer" };
            // Act
            jobTitle.Name = "Architect";
            // Assert
            Assert.AreEqual("Architect", jobTitle.Name);
        }

        [TestMethod]
        public void CanChangeJobTitleDescription()
        {
            // Arrange
            var jobTitle = new JobTitle { Description = "Developer" };
            // Act
            jobTitle.Description = "Architect";
            // Assert
            Assert.AreEqual("Architect", jobTitle.Description);
        }

        [TestMethod]
        public void CanDeleteJobTitle()
        {
            // Arrange
            var jobTitle = new JobTitle { Name = "Developer" };
            // Act
            jobTitle.Name = null;
            // Assert
            Assert.IsNull(jobTitle.Name);
        }
    }
}