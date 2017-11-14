# output grpc from proto file
PROTOC=../../tools/Grpc.Tools.1.7.1/tools/macosx_x64/protoc
PLUGIN=../../tools/Grpc.Tools.1.7.1/tools/macosx_x64/grpc_csharp_plugin

$PROTOC --csharp_out .  --grpc_out . --plugin=protoc-gen-grpc=$PLUGIN MiniGrpc.proto
