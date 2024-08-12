using System;

namespace Models.DbEntities.JsonEntities.WaitList;

public record PreferredTimeRange(
    DateTime Start,
    DateTime End);