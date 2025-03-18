using System.Runtime.Serialization;

namespace Movieminds.Client.Enums;

public enum MovieSortBy
{
    [EnumMember(Value = "original_title.asc")]
    OriginalTitleAsc,

    [EnumMember(Value = "original_title.desc")]
    OriginalTitleDesc,

    [EnumMember(Value = "popularity.asc")]
    PopularityAsc,

    [EnumMember(Value = "popularity.desc")]
    PopularityDesc,

    [EnumMember(Value = "primary_release_date.asc")]
    PrimaryReleaseDateAsc,

    [EnumMember(Value = "primary_release_date.desc")]
    PrimaryReleaseDateDesc,
}
