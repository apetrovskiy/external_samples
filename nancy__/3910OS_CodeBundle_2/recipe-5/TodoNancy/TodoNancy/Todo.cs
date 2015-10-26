namespace TodoNancy
{
  using ProtoBuf;

  [ProtoContract]
  public class Todo
  {
    [ProtoMember(1)]
    public long id { get; set; }
    [ProtoMember(2)]
    public string title { get; set; }
    [ProtoMember(3)]
    public int order { get; set; }
    [ProtoMember(4)]
    public bool completed { get; set; }
  }
}