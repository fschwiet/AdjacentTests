# Target-Adjacent Tests

## Summary

This git repository demonstrates how a C# project and its corresponding test project can be configured to allow the test files to be in the same location as the code they test. This makes it easier to see what is tested, to find tests for a component, and to keep the test folder structure consistent with the code structure.

I am not claiming all tests should be adjacent like this, some tests might still live in the test project folder. A lot of test utilities shared across tests, in particular, might still live in the test project folder.

Anyhow, you can see Foo.cs and FooTests.cs live next to each other in the same folder. Nice and easy.

Well, while worth it overall, our developer environment may not know what to do with this. So I also wanted to use this repository to document issues using rider in such a situation. I don't know if Visual Studio fairs better or worse as I haven't tried it.

## Setup

The configuration is achieved in the csproj files. We exclude test files from the library project and then import them into the test project. I also change the root namespace of the test project to match the targetted Library, this is optional. Either way the test files placed in Library project's folder would want to use the same namespaces as the Library code. 

### ./Library/Library.csproj
```
  <ItemGroup>
    <Compile Remove="**\*Test.cs" />
    <Compile Remove="**\*Tests.cs" />
    <Compile Remove="**\*TestHelper.cs" />
  </ItemGroup>
```
### ./LibraryTests/LibraryTests.csproj
```
  <ItemGroup>
    <Compile Include="../Library/**/*Test.cs" />
    <Compile Include="../Library/**/*Tests.cs" />
    <Compile Include="../Library/**/*TestHelper.cs" />
  </ItemGroup>
```


## Miscellaneous notes

The project explorer in Rider needs to be set to "File System" to see the source and test files next to each other.

## Library.csproj file is updated to erroneously updated to include test files when adding test files

### Steps to reproduce

- From the project explorer, configured to "File System" in the dropdown at its top, right click on folder Library/RFC309 and "Add>Class". Name the class "NewTests.cs"
- Add the test definition to the newly created file:
```
namespace Library.RFC3092;

public class NewTests
{
	[Fact]
	public void CanRunATest()
	{
		
	}
}
```

## Expected result

- The new test should build and run as part of the test project.
- - The Library.csproj file should not have been modified. The files disposition is already appropriately configured by the csproj files. Note that by removing the modification one can build and run the test.

## ActualResult

- JetBrain's Rider has modified the Library.csproj file to include the test file, rather than let it be included by the test library. The added line is the final Include here:
    ```
      <ItemGroup>
        <Compile Remove="**\*Test.cs" />
        <Compile Remove="**\*Tests.cs" />
        <Compile Remove="**\*TestHelper.cs" />
        <Compile Include="RFC3092\NewTests.cs" />
      </ItemGroup>
    ```
  
## "Move to file", a command meant to split a class into its own file, displaces the file into the wrong project.

### Steps to reproduce

1. Observe file ./Library/RFC3029/BarTests.cs contains some test code and the code to be tested. I like to start new files like this to get some initial code with test coverage working.
2. For the class Bar in BarTests.cs, do the "Move to Bar.cs" refactoring.

## Actual result

- The Bar class is moved to its own file, but it is placed in folder ./LibraryTests instead of remaining in ./Library/RFC3092.
- The created file still uses the RFC3092 namespace which doesn't even correspond to its position.

- Here is the created file ./LibraryTests/Bar.cs
  ```
  namespace Library.RFC3092;
  
  public class Bar
  {
      public int Divide(int a, int b) => a / b;
  }
  ```
  
## Expected result:

- The new Bar.cs file should be created in the original directory "./Library/RFC3092" as is.
- The csprojs files should not be modified. The existing csproj rules will treat the class as part of the Library project, as expect.