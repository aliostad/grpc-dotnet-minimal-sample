Minimal gRPC sample for C#
========================

OVERVIEW
-------------
This sample is based on official gRPC samples for C# but modified for .NET Core 2.0 and displays features gRPC features in the same application.

This sample is targeted at Mac/Linux users.

PREREQUISITES
-------------

- The [.NET Core SDK](https://www.microsoft.com/net/core).

- Get gRPC tools
  - move to root repo directory: `cd grpc-dotnet-minimal-sample`
  - run `mkdir tools && cd tools && nuget install Grpc.Tools`
- Auto-generate files from .proto file:
  - move to *Common* project `cd ../src/MiniGrpc.Common`
  - run `./generate_protos.sh` (it has run before and sources stored in git but in case you would like to change the IDL and try it)

BUILD
-------

From the `grpc-dotnet-minimal-sample/src` directory:

- `dotnet restore`

- `dotnet build`

Try the sameple
-------

- Run the server

  ```
  dotnet run --framework netcoreapp2.0
  ```

- Run the client for `Add` operation

  ```
  dotnet run --framework netcoreapp2.0 add
  ```
and you can run it for travel operation which uses client streaming:

```
dotnet run --framework netcoreapp2.0 travel
```
