using System;
using System.Collections.Generic;

namespace UpStart.Domain.Requests.Notification
{
    public class InsertRealTimeBroadcastNotificationRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public List<Guid> Users { get; set; }
    }
}
