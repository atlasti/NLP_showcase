// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: HelloWorld.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace SSD.ATLASti.Common.HelloWorld.Model {

  /// <summary>Holder for reflection information generated from HelloWorld.proto</summary>
  public static partial class HelloWorldReflection {

    #region Descriptor
    /// <summary>File descriptor for HelloWorld.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static HelloWorldReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChBIZWxsb1dvcmxkLnByb3RvEgdhdGxhc3RpIh0KCkhlbGxvV29ybGQSDwoH",
            "bWVzc2FnZRgBIAEoCSIqChpTYXlIZWxsb0FnYWluQWN0aW9uUmVxdWVzdBIM",
            "CgRuYW1lGAEgASgJInkKGFNheUhlbGxvQWdhaW5BY3Rpb25SZXBseRIbChNw",
            "ZXJjZW50YWdlX2NvbXBsZXRlGAEgASgCEhYKDnN0YXR1c19tZXNzYWdlGAIg",
            "ASgJEigKC2hlbGxvX3dvcmxkGAMgASgLMhMuYXRsYXN0aS5IZWxsb1dvcmxk",
            "IiUKFVNheUhlbGxvQWN0aW9uUmVxdWVzdBIMCgRuYW1lGAEgASgJIj8KE1Nh",
            "eUhlbGxvQWN0aW9uUmVwbHkSKAoLaGVsbG9fd29ybGQYASABKAsyEy5hdGxh",
            "c3RpLkhlbGxvV29ybGQyuAEKEUhlbGxvV29ybGRTZXJ2aWNlElkKDVNheUhl",
            "bGxvQWdhaW4SIy5hdGxhc3RpLlNheUhlbGxvQWdhaW5BY3Rpb25SZXF1ZXN0",
            "GiEuYXRsYXN0aS5TYXlIZWxsb0FnYWluQWN0aW9uUmVwbHkwARJICghTYXlI",
            "ZWxsbxIeLmF0bGFzdGkuU2F5SGVsbG9BY3Rpb25SZXF1ZXN0GhwuYXRsYXN0",
            "aS5TYXlIZWxsb0FjdGlvblJlcGx5QiyiAgNTU0SqAiNTU0QuQVRMQVN0aS5D",
            "b21tb24uSGVsbG9Xb3JsZC5Nb2RlbGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld), global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld.Parser, new[]{ "Message" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloAgainActionRequest), global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloAgainActionRequest.Parser, new[]{ "Name" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloAgainActionReply), global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloAgainActionReply.Parser, new[]{ "PercentageComplete", "StatusMessage", "HelloWorld" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloActionRequest), global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloActionRequest.Parser, new[]{ "Name" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloActionReply), global::SSD.ATLASti.Common.HelloWorld.Model.SayHelloActionReply.Parser, new[]{ "HelloWorld" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class HelloWorld : pb::IMessage<HelloWorld> {
    private static readonly pb::MessageParser<HelloWorld> _parser = new pb::MessageParser<HelloWorld>(() => new HelloWorld());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HelloWorld> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorldReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloWorld() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloWorld(HelloWorld other) : this() {
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HelloWorld Clone() {
      return new HelloWorld(this);
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 1;
    private string message_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as HelloWorld);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(HelloWorld other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Message.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(HelloWorld other) {
      if (other == null) {
        return;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SayHelloAgainActionRequest : pb::IMessage<SayHelloAgainActionRequest> {
    private static readonly pb::MessageParser<SayHelloAgainActionRequest> _parser = new pb::MessageParser<SayHelloAgainActionRequest>(() => new SayHelloAgainActionRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SayHelloAgainActionRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorldReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloAgainActionRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloAgainActionRequest(SayHelloAgainActionRequest other) : this() {
      name_ = other.name_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloAgainActionRequest Clone() {
      return new SayHelloAgainActionRequest(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SayHelloAgainActionRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SayHelloAgainActionRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SayHelloAgainActionRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SayHelloAgainActionReply : pb::IMessage<SayHelloAgainActionReply> {
    private static readonly pb::MessageParser<SayHelloAgainActionReply> _parser = new pb::MessageParser<SayHelloAgainActionReply>(() => new SayHelloAgainActionReply());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SayHelloAgainActionReply> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorldReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloAgainActionReply() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloAgainActionReply(SayHelloAgainActionReply other) : this() {
      percentageComplete_ = other.percentageComplete_;
      statusMessage_ = other.statusMessage_;
      helloWorld_ = other.helloWorld_ != null ? other.helloWorld_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloAgainActionReply Clone() {
      return new SayHelloAgainActionReply(this);
    }

    /// <summary>Field number for the "percentage_complete" field.</summary>
    public const int PercentageCompleteFieldNumber = 1;
    private float percentageComplete_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float PercentageComplete {
      get { return percentageComplete_; }
      set {
        percentageComplete_ = value;
      }
    }

    /// <summary>Field number for the "status_message" field.</summary>
    public const int StatusMessageFieldNumber = 2;
    private string statusMessage_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string StatusMessage {
      get { return statusMessage_; }
      set {
        statusMessage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "hello_world" field.</summary>
    public const int HelloWorldFieldNumber = 3;
    private global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld helloWorld_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld HelloWorld {
      get { return helloWorld_; }
      set {
        helloWorld_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SayHelloAgainActionReply);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SayHelloAgainActionReply other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(PercentageComplete, other.PercentageComplete)) return false;
      if (StatusMessage != other.StatusMessage) return false;
      if (!object.Equals(HelloWorld, other.HelloWorld)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (PercentageComplete != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(PercentageComplete);
      if (StatusMessage.Length != 0) hash ^= StatusMessage.GetHashCode();
      if (helloWorld_ != null) hash ^= HelloWorld.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (PercentageComplete != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(PercentageComplete);
      }
      if (StatusMessage.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(StatusMessage);
      }
      if (helloWorld_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(HelloWorld);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (PercentageComplete != 0F) {
        size += 1 + 4;
      }
      if (StatusMessage.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(StatusMessage);
      }
      if (helloWorld_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(HelloWorld);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SayHelloAgainActionReply other) {
      if (other == null) {
        return;
      }
      if (other.PercentageComplete != 0F) {
        PercentageComplete = other.PercentageComplete;
      }
      if (other.StatusMessage.Length != 0) {
        StatusMessage = other.StatusMessage;
      }
      if (other.helloWorld_ != null) {
        if (helloWorld_ == null) {
          HelloWorld = new global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld();
        }
        HelloWorld.MergeFrom(other.HelloWorld);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 13: {
            PercentageComplete = input.ReadFloat();
            break;
          }
          case 18: {
            StatusMessage = input.ReadString();
            break;
          }
          case 26: {
            if (helloWorld_ == null) {
              HelloWorld = new global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld();
            }
            input.ReadMessage(HelloWorld);
            break;
          }
        }
      }
    }

  }

  public sealed partial class SayHelloActionRequest : pb::IMessage<SayHelloActionRequest> {
    private static readonly pb::MessageParser<SayHelloActionRequest> _parser = new pb::MessageParser<SayHelloActionRequest>(() => new SayHelloActionRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SayHelloActionRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorldReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloActionRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloActionRequest(SayHelloActionRequest other) : this() {
      name_ = other.name_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloActionRequest Clone() {
      return new SayHelloActionRequest(this);
    }

    /// <summary>Field number for the "name" field.</summary>
    public const int NameFieldNumber = 1;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SayHelloActionRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SayHelloActionRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Name != other.Name) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Name.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Name);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SayHelloActionRequest other) {
      if (other == null) {
        return;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Name = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class SayHelloActionReply : pb::IMessage<SayHelloActionReply> {
    private static readonly pb::MessageParser<SayHelloActionReply> _parser = new pb::MessageParser<SayHelloActionReply>(() => new SayHelloActionReply());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<SayHelloActionReply> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorldReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloActionReply() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloActionReply(SayHelloActionReply other) : this() {
      helloWorld_ = other.helloWorld_ != null ? other.helloWorld_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public SayHelloActionReply Clone() {
      return new SayHelloActionReply(this);
    }

    /// <summary>Field number for the "hello_world" field.</summary>
    public const int HelloWorldFieldNumber = 1;
    private global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld helloWorld_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld HelloWorld {
      get { return helloWorld_; }
      set {
        helloWorld_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as SayHelloActionReply);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(SayHelloActionReply other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(HelloWorld, other.HelloWorld)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (helloWorld_ != null) hash ^= HelloWorld.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (helloWorld_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(HelloWorld);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (helloWorld_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(HelloWorld);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(SayHelloActionReply other) {
      if (other == null) {
        return;
      }
      if (other.helloWorld_ != null) {
        if (helloWorld_ == null) {
          HelloWorld = new global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld();
        }
        HelloWorld.MergeFrom(other.HelloWorld);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (helloWorld_ == null) {
              HelloWorld = new global::SSD.ATLASti.Common.HelloWorld.Model.HelloWorld();
            }
            input.ReadMessage(HelloWorld);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
