﻿using System;
namespace Pets.Databases
{
	public class MongoConnection
	{
        public string Connection { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}

