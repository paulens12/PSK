﻿namespace PSK.Domain
{
    public class Address : Entity
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public Office Office { get; set; }

        public Accommodation Accommodation { get; set; }
    }
}