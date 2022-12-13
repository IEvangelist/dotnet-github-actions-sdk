// Copyright (c) David Pine. All rights reserved.
// Licensed under the MIT License.

namespace Actions.IO.Tests;

public sealed class OperationTests : IClassFixture<TempFolderTestFixture>
{
    readonly TempFolderTestFixture _fixture;

    public OperationTests(TempFolderTestFixture fixture) => _fixture = fixture;

    [Fact]
    public void CopiesFilesWithNoFlags()
    {
        // Arrange
        var root = Path.Combine(_fixture.TempFolder, "copy_with_no_flags");
        var sourceFile = Path.Combine(root, "cp_source");
        var targetFile = Path.Combine(root, "cp_target");
        IOperations operations = new Operations();
        operations.MakeDirectory(root);
        File.WriteAllText(sourceFile, "test file content");

        // Act
        operations.Copy(sourceFile, targetFile);

        // Assert
        Assert.Equal("test file content", File.ReadAllText(targetFile));
    }

    [Fact]
    public void CopiesFilesUsingForce()
    {
        // Arrange
        var root = Path.Combine(_fixture.TempFolder, "copy_with_-f");
        var sourceFile = Path.Combine(root, "cp_source");
        var targetFile = Path.Combine(root, "cp_target");
        IOperations operations = new Operations();
        operations.MakeDirectory(root);
        File.WriteAllText(sourceFile, "test file content");

        // Act
        operations.Copy(sourceFile, targetFile);

        // Assert
        Assert.Equal("test file content", File.ReadAllText(targetFile));
    }

    [Fact]
    public void CopiesFileIntoDirectory()
    {
        // Arrange
        var root = Path.Combine(_fixture.TempFolder, "copy_file_to_directory");
        var sourceFile = Path.Combine(root, "cp_source");
        var targetDirectory = Path.Combine(root, "cp_target");
        var targetFile = Path.Combine(targetDirectory, "cp_source");
        IOperations operations = new Operations();
        operations.MakeDirectory(targetDirectory);
        File.WriteAllText(sourceFile, "test file content");

        // Act
        operations.Copy(sourceFile, targetFile, new(false, true));

        // Assert
        Assert.Equal("test file content", File.ReadAllText(targetFile));
    }

    [Fact]
    public void TryCopyingToExistingFileWithFlagN()
    {
        // Arrange
        var root = Path.Combine(_fixture.TempFolder, "copy_to_existing");
        var sourceFile = Path.Combine(root, "cp_source");
        var targetFile = Path.Combine(root, "cp_target");
        IOperations operations = new Operations();
        operations.MakeDirectory(root);
        File.WriteAllText(sourceFile, "test file content");
        File.WriteAllText(targetFile, "correct content");

        // Act
        operations.Copy(sourceFile, targetFile, new(false, false));

        // Assert
        Assert.Equal("correct content", File.ReadAllText(targetFile));
    }

    [Fact(Skip = "This is currently incorrectly implemented.")]
    public void CopiesDirectoryIntoExistingDestinationWithFlagR()
    {
        // Arrange
        var root = Path.Combine(_fixture.TempFolder, "cp_with_-r_existing_dest");
        var sourceFolder = Path.Combine(root, "cp_source");
        var sourceFile = Path.Combine(sourceFolder, "cp_source_file");
        var targetFolder = Path.Combine(root, "cp_target");
        var targetFile = Path.Combine(targetFolder, "cp_source", "cp_source_file");
        IOperations operations = new Operations();
        operations.MakeDirectory(sourceFolder);
        File.WriteAllText(sourceFile, "test file content");
        operations.MakeDirectory(targetFolder);

        // Act
        operations.Copy(sourceFolder, targetFolder, new(true));

        // Assert
        Assert.Equal("test file content", File.ReadAllText(targetFile));
    }
}