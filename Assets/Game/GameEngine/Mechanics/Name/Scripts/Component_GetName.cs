using Elementary;


public sealed class Component_GetName : IComponent_GetName
{
    public string Name
    {
        get { return this.name.Value; }
    }

    private readonly IValue<string> name;

    public Component_GetName(IValue<string> name)
    {
        this.name = name;
    }
}