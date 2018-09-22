﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMBot.Data.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        public string DiscordUserID { get; set; }

        public bool? Featured { get; set; }

        public bool? Blacklisted { get; set; }

        public UserType UserType { get; set; }

        public virtual Settings Settings { get; set; }

        public ICollection<Guild> Guilds { get; set; }
    }
}