using BinaryTreeTask.BL;
using FluentAssertions;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;

namespace BinaryTreeTask.Tests
{
    public class BinaryTreeTests
    {
        [Fact]
        public void Returns_correct_max_sum_of_odd_event_sequential_nodes_path_1()
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

            var binaryTreeReader = new BinaryTreeReader(mockFileSystem);
            var sut = binaryTreeReader.GetBinaryTree(inputFilePath).Value;

            //Act
            var actualResult = sut.GetMaxSumOfOddEventSequentialNodes();


            //Assert
            actualResult.IsSuccess.Should().BeTrue();
            actualResult.IsFailure.Should().BeFalse();
            actualResult.Value.Should().Be(16);
        }

        [Fact]
        public void Returns_correct_max_sum_of_odd_event_sequential_nodes_path_2()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("1 5 9");
            textLfileLines.AppendLine("20 5 2 3");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var binaryTreeReader = new BinaryTreeReader(mockFileSystem);
            var sut = binaryTreeReader.GetBinaryTree(inputFilePath).Value;

            //Act
            var actualResult = sut.GetMaxSumOfOddEventSequentialNodes();


            //Assert
            actualResult.IsSuccess.Should().BeTrue();
            actualResult.IsFailure.Should().BeFalse();
            actualResult.Value.Should().Be(30);
        }

        [Fact]
        public void Returns_correct_max_sum_of_odd_event_sequential_nodes_path_3()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 8");
            textLfileLines.AppendLine("1 5 9");
            textLfileLines.AppendLine("4 5 2 2");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var binaryTreeReader = new BinaryTreeReader(mockFileSystem);
            var sut = binaryTreeReader.GetBinaryTree(inputFilePath).Value;

            //Act
            var actualResult = sut.GetMaxSumOfOddEventSequentialNodes();


            //Assert
            actualResult.IsSuccess.Should().BeTrue();
            actualResult.IsFailure.Should().BeFalse();
            actualResult.Value.Should().Be(20);
        }

        [Fact]
        public void Returns_error_then_no_valid_path_exist()
        {
            //Arrange
            const string inputDir = @"c:\root\in";
            const string inputFileName = "myfile.txt";
            var inputFilePath = Path.Combine(inputDir, inputFileName);

            var textLfileLines = new StringBuilder();
            textLfileLines.AppendLine("1");
            textLfileLines.AppendLine("8 9");
            textLfileLines.AppendLine("2 2 9");
            textLfileLines.AppendLine("4 5 2 3");

            var mockInputFile = new MockFileData(textLfileLines.ToString());

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(inputFilePath, mockInputFile);

            var binaryTreeReader = new BinaryTreeReader(mockFileSystem);
            var sut = binaryTreeReader.GetBinaryTree(inputFilePath).Value;

            //Act
            var actualResult = sut.GetMaxSumOfOddEventSequentialNodes();


            //Assert
            actualResult.IsSuccess.Should().BeFalse();
            actualResult.IsFailure.Should().BeTrue();
            actualResult.Error.Should().Be("Binary tree does not contain valid path.");
        }

        [Fact]
        public void Returns_correct_max_sum_of_odd_event_sequential_nodes_when_reads_real_file()
        {
            //Arrange
            var binaryTreeReader = new BinaryTreeReader();
            var sut = binaryTreeReader.GetBinaryTree("testdata2.txt").Value;

            //Act
            var actualResult = sut.GetMaxSumOfOddEventSequentialNodes();

            //Assert
            actualResult.IsSuccess.Should().BeTrue();
            actualResult.IsFailure.Should().BeFalse();
            actualResult.Value.Should().Be(16);
        }
    }
}
