using System.Collections.Generic;

namespace CommandFramework.Commands
{
    public interface IParameterDescription
    {
        string Name { get; }

        int PositionIndex { get; }

        string Description { get; set; }

        bool AllowsDefaultValue { get; }

        object DefaultValue { get; }

        ICollection<string> Synonyms { get; }

        bool IsCollection { get; }
    }
}