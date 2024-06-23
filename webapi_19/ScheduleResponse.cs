using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace webapi_02
{
    public class ScheduleResponse
    {
        public List<Schedule> TherapyEvents { get; set; } = new List<Schedule>();

    }
}