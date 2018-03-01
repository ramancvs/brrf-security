﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

namespace BRRF.IdentityServer.Repository.Entities
{
    public class ClientSecret : Secret
    {
        public Client Client { get; set; }
    }
}