using System;
using System.Collections.Generic;

namespace WeekendRoguelike.AI.FactionSystem
{
    public class Faction : IEquatable<Faction>
    {
        #region Private Fields

        private static readonly Dictionary<string, Faction> factionLookup =
            new Dictionary<string, Faction>();

        private readonly int id;
        private readonly string name;

        private Dictionary<Faction, Relationship> relationship =
            new Dictionary<Faction, Relationship>();

        #endregion Private Fields

        #region Private Constructors

        private Faction(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Name must not be null or whitespace.");
            this.name = string.Intern(name);
            id = this.name.GetHashCode();
        }

        #endregion Private Constructors

        #region Public Properties

        public string Name => name;

        #endregion Public Properties

        #region Public Methods

        public static implicit operator Faction(string factionName)
        {
            if (factionLookup.TryGetValue(factionName, out var faction))
                return faction;
            else
            {
                var output = new Faction(factionName);
                factionLookup.Add(factionName, output);
                return output;
            }
        }

        public static void SetRelationship(
            Faction factionA, Faction factionB, int value)
        {
            Relationship relate;
            if (factionA.relationship.TryGetValue(factionB, out relate) == false)
            {
                relate = new Relationship();
                factionA.relationship.Add(factionB, relate);
                factionB.relationship.Add(factionA, relate);
            }
            relate.Value = value;
        }

        public override bool Equals(object obj)
        {
            return
                obj is Faction other &&
                Equals(other);
        }

        public bool Equals(Faction other)
        {
            return id == other.id;
        }

        public override int GetHashCode()
        {
            return id;
        }

        public bool TryGetRelationshipValue(Faction factionB, out int value)
        {
            Relationship relate;
            if (relationship.TryGetValue(factionB, out relate))
            {
                value = relate.Value;
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }

        #endregion Public Methods

        #region Private Classes

        private class Relationship
        {
            #region Private Fields

            private int value;

            #endregion Private Fields

            #region Public Properties

            public int Value
            {
                get => Math.Max(-100, Math.Min(100, value));
                set => this.value = Math.Max(-100, Math.Min(100, value));
            }

            #endregion Public Properties
        }

        #endregion Private Classes
    }
}
