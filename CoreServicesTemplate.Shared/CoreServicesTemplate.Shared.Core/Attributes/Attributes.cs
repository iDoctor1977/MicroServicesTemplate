using System;
using System.Collections.Generic;

namespace CoreServicesTemplate.Shared.Core.Attributes
{
    public class RootAttribute : Attribute
    {
        private readonly List<string> _threeStepNames = new List<string>();

        public RootAttribute() { }

        public RootAttribute(string rootName)
        {
            GetRootName = rootName;
        }

        public string GetRootName { get; }

        public List<string> GetThreeStructure => _threeStepNames;

        public void AddStepNameToThree(string stepName)
        {
            _threeStepNames.Add(stepName);
        }
    }

    public class LeafAttribute : Attribute
    {
        public LeafAttribute(string rootFatherName)
        {
            GetRootFatherName = rootFatherName;
        }

        public string GetRootFatherName { get; }
    }
}
