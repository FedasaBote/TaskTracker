
using Blog.Core;
using Blog.Data;
using Blog.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blog.Tests
{
    public class PostManagerTests
    {
        [Fact]
        public void CreatePost_ValidInput_PostCreatedSuccessfully()
        {
            // Arrange
            var contextMock = new Mock<BlogDbContext>();
            var postManager = new PostManager(contextMock.Object);
            var title = "Test Title";
            var content = "Test Content";

            // Act
            postManager.CreatePost(title, content);

            // Assert
            contextMock.Verify(c => c.Posts.Add(It.IsAny<Post>()), Times.Once);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Theory]
        [InlineData("", "Test Content")]
        [InlineData("Test Title", "")]
        public void CreatePost_EmptyInput_ThrowsArgumentException(string title, string content)
        {
            // Arrange
            var contextMock = new Mock<BlogDbContext>();
            var postManager = new PostManager(contextMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => postManager.CreatePost(title, content));
            Assert.Equal("Title and content cannot be empty.", ex.Message);
        }

        [Fact]
        public void CreatePost_ExceptionInSaveChanges_ThrowsException()
        {
            // Arrange
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.SaveChanges()).Throws(new Exception("Test exception"));
            var postManager = new PostManager(contextMock.Object);
            var title = "Test Title";
            var content = "Test Content";

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => postManager.CreatePost(title, content));
            Assert.Equal("Error: Test exception", ex.Message);
            Assert.Equal("Test exception", ex.InnerException.Message);
        }

        [Fact]
        public void GetAllPosts_ReturnsAllPosts()
        {
            // Arrange
            var posts = new List<Post>
            {
                new Post { PostId = 1, Title = "Title 1", Content = "Content 1", Created = DateTime.Now },
                new Post { PostId = 2, Title = "Title 2", Content = "Content 2", Created = DateTime.Now },
                new Post { PostId = 3, Title = "Title 3", Content = "Content 3", Created = DateTime.Now }
            };
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts).Returns((Microsoft.EntityFrameworkCore.DbSet<Post>)posts.AsQueryable());
            var postManager = new PostManager(contextMock.Object);

            // Act
            var result = postManager.GetAllPosts();

            // Assert
            Assert.Equal(posts.Count, result.Count);
        }

        [Fact]
        public void GetPost_ExistingId_ReturnsPost()
        {
            // Arrange
            var postId = 1;
            var post = new Post { PostId = postId, Title = "Test Title", Content = "Test Content", Created = DateTime.Now };
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts.FirstOrDefault(p => p.PostId == postId)).Returns(post);
            var postManager = new PostManager(contextMock.Object);

            // Act
            var result = postManager.GetPost(postId);

            // Assert
            Assert.Equal(post, result);
        }

        [Fact]
        public void GetPost_NonExistingId_ReturnsNull()
        {
            // Arrange
            var postId = 1;
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts.FirstOrDefault(p => p.PostId == postId)).Returns((Post)null);
            var postManager = new PostManager(contextMock.Object);

            // Act
            var result = postManager.GetPost(postId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void UpdatePost_ExistingId_UpdatesPost()
        {
            // Arrange
            var postId = 1;
            var newTitle = "Updated Title";
            var newContent = "Updated Content";
            var post = new Post { PostId = postId, Title = "Test Title", Content = "Test Content", Created = DateTime.Now };
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts.FirstOrDefault(p => p.PostId == postId)).Returns(post);
            var postManager = new PostManager(contextMock.Object);

            // Act
            postManager.UpdatePost(postId, newTitle, newContent);

            // Assert
            Assert.Equal(newTitle, post.Title);
            Assert.Equal(newContent, post.Content);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void UpdatePost_NonExistingId_ThrowsArgumentException()
        {
            // Arrange
            var postId = 1;
            var newTitle = "Updated Title";
            var newContent = "Updated Content";
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts.FirstOrDefault(p => p.PostId == postId)).Returns((Post)null);
            var postManager = new PostManager(contextMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => postManager.UpdatePost(postId, newTitle, newContent));
            Assert.Equal($"Post with ID {postId} not found.", ex.Message);
        }

        [Fact]
        public void DeletePost_ExistingId_DeletesPost()
        {
            // Arrange
            var postId = 1;
            var post = new Post { PostId = postId, Title = "Test Title", Content = "Test Content", Created = DateTime.Now };
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts.FirstOrDefault(p => p.PostId == postId)).Returns(post);
            var postManager = new PostManager(contextMock.Object);

            // Act
            postManager.DeletePost(postId);

            // Assert
            contextMock.Verify(c => c.Posts.Remove(post), Times.Once);
            contextMock.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeletePost_NonExistingId_ThrowsArgumentException()
        {
            // Arrange
            var postId = 1;
            var contextMock = new Mock<BlogDbContext>();
            contextMock.Setup(c => c.Posts.FirstOrDefault(p => p.PostId == postId)).Returns((Post)null);
            var postManager = new PostManager(contextMock.Object);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => postManager.DeletePost(postId));
            Assert.Equal($"Post with ID {postId} not found.", ex.Message);
        }
    }
}


