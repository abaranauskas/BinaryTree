using BinaryTreeTask.BL;
using FluentAssertions;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;

namespace BinaryTreeTask.Tests
{
    public class BinaryTreeReaderTests
    {
        [Fact]
        public void Returns_error_then_file_does_not_exist()
        {
            //Arrange
            var sut = new BinaryTreeReader();

            //Act
            var actualResult = sut.GetBinaryTree("nonexistingfile.txt");

            //Assert
            actualResult.IsFailure.Should().BeTrue();            
            actualResult.IsSuccess.Should().BeFalse();            
            actualResult.Error.Should().StartWith("Could not find file");            
        }

        [Fact]
        public void Returns_error_when_first_line_has_more_than_one_value()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("1 5 9");
            textLfileLines.AppendLine("4 5 2 3");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var sut = new BinaryTreeReader(mockFileSystem);

            //Act
            var actualResult = sut.GetBinaryTree(inputFilePath);

            //Assert
            actualResult.IsFailure.Should().BeTrue();
            actualResult.IsSuccess.Should().BeFalse();
            actualResult.Error.Should().Be("Invalid binary tree data structure.");
        }


        [Fact]
        public void Returns_error_when_line_has_less_values_comparing_to_previous_line()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("1 5 9");
            textLfileLines.AppendLine("4 5");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var sut = new BinaryTreeReader(mockFileSystem);

            //Act
            var actualResult = sut.GetBinaryTree(inputFilePath);

            //Assert
            actualResult.IsFailure.Should().BeTrue();
            actualResult.IsSuccess.Should().BeFalse();
            actualResult.Error.Should().Be("Invalid binary tree data structure.");
        }

        [Fact]
        public void Returns_error_when_line_has_same_number_of_values_comparing_to_previous_line()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("1 5 9");
            textLfileLines.AppendLine("4 5 2");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var sut = new BinaryTreeReader(mockFileSystem);

            //Act
            var actualResult = sut.GetBinaryTree(inputFilePath);

            //Assert
            actualResult.IsFailure.Should().BeTrue();
            actualResult.IsSuccess.Should().BeFalse();
            actualResult.Error.Should().Be("Invalid binary tree data structure.");
        }
        
        [Fact]
        public void Returns_error_when_line_has_two_or_more_values_more_comparing_to_previous_line()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("1 5 9 10");
            textLfileLines.AppendLine("4 5 2 3");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var sut = new BinaryTreeReader(mockFileSystem);

            //Act
            var actualResult = sut.GetBinaryTree(inputFilePath);

            //Assert
            actualResult.IsFailure.Should().BeTrue();
            actualResult.IsSuccess.Should().BeFalse();
            actualResult.Error.Should().Be("Invalid binary tree data structure.");
        }

        [Fact]
        public void Returns_binary_tree_object_when_input_data_is_valid()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("1 5 9");
            textLfileLines.AppendLine("4 5 2 3");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var sut = new BinaryTreeReader(mockFileSystem);

            //Act
            var actualResult = sut.GetBinaryTree(inputFilePath);

            //Assert
            actualResult.IsSuccess.Should().BeTrue();
            actualResult.IsFailure.Should().BeFalse();
            actualResult.Value.GetType().Should().Be(typeof(BinaryTree));
        }
    }
}
