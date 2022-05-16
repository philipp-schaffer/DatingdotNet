﻿using System;
using System.Collections.Generic;

namespace ChatBox.Model
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public string? MessageContent { get; set; }
        public int ChatId { get; set; }

        public virtual Chat Chat { get; set; } = null!;
    }
}
