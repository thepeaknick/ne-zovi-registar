using System;

namespace NeZoviReg.Migrations.Ef.Extensions;
public readonly struct ReplacePair : IEquatable<ReplacePair>
    {
        public ReplacePair(string searchString, string replaceString)
        {
            SearchString = searchString;
            ReplaceString = replaceString;
        }

        public string SearchString { get; }

        public string ReplaceString { get; }

        public static bool operator ==(ReplacePair left, ReplacePair right) => left.Equals(right);

        public static bool operator !=(ReplacePair left, ReplacePair right) => !(left == right);

        public bool Equals(ReplacePair other) => SearchString == other.SearchString && ReplaceString == other.ReplaceString;

        public override bool Equals(object obj) => obj is ReplacePair other && Equals(other);

        public override int GetHashCode()
        {
#pragma warning disable CA1307 // Specify StringComparison
            unchecked
            {
                return ((SearchString != null ? SearchString.GetHashCode() : 0) * 397) ^
                       (ReplaceString != null ? ReplaceString.GetHashCode() : 0);
            }
#pragma warning restore CA1307 // Specify StringComparison
        }
    }
