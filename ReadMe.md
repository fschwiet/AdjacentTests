# Target-Adjacent Tests

## Summary

This git repository demonstrates how a C# project and its corresponding test project can be configured to allow the test files to be in the same location as the code they test. This makes it easier to see what is tested, to find tests for a component, and to keep the test folder structure consistent with the code structure.

I am not claiming all tests should be adjacent like this, some tests might still live in the test project folder. A lot of test utilities shared across tests, in particular, might still live in the test project folder.

Anyhow, you can see Foo.cs and FooTests.cs live next to each other in the same folder. Nice and easy.

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