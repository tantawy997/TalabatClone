﻿namespace Core.entites.OrderAggragate
{
    public class ShippingAddress
    {
        public ShippingAddress() { }
        public ShippingAddress(string firstName, string lastName, string street, string city, string state, string zipcode)
        {
            FirstName = firstName;
            LastName = lastName;
            this.street = street;
            this.city = city;
            this.state = state;
            this.zipcode = zipcode;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string street { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zipcode { get; set; }
        
    }
}