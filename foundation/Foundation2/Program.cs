using System;
using System.Collections.Generic;

namespace ProductOrderingSystem
{
    // Address class
    public class Address
    {
        private string street;
        private string city;
        private string stateOrProvince;
        private string country;

        public Address(string street, string city, string stateOrProvince, string country)
        {
            this.street = street;
            this.city = city;
            this.stateOrProvince = stateOrProvince;
            this.country = country;
        }

        // Returns true if the address is in the USA
        public bool IsInUSA()
        {
            return country == "USA";
        }

        // Returns the full address as a string
        public string GetFullAddress()
        {
            return $"{street}\n{city}, {stateOrProvince}\n{country}";
        }
    }

    // Customer class
    public class Customer
    {
        private string name;
        private Address address;

        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        // Returns true if the customer lives in the USA
        public bool LivesInUSA()
        {
            return address.IsInUSA();
        }

        // Returns the name and address as a string
        public string GetCustomerDetails()
        {
            return $"{name}\n{address.GetFullAddress()}";
        }
    }

    // Product class
    public class Product
    {
        private string name;
        private string productId;
        private double price;
        private int quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        // Returns the total cost of the product
        public double GetTotalCost()
        {
            return price * quantity;
        }

        // Returns product details for packing label
        public string GetPackingLabel()
        {
            return $"Product: {name}, ID: {productId}";
        }
    }

    // Order class
    public class Order
    {
        private List<Product> products;
        private Customer customer;

        public Order(Customer customer)
        {
            this.customer = customer;
            products = new List<Product>();
        }

        // Add a product to the order
        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        // Returns the total cost of the order (including shipping)
        public double GetTotalCost()
        {
            double total = 0;
            foreach (Product product in products)
            {
                total += product.GetTotalCost();
            }

            // Add shipping cost: $5 for USA, $35 for international
            total += customer.LivesInUSA() ? 5 : 35;

            return total;
        }

        // Returns the packing label (name and product id of each product)
        public string GetPackingLabel()
        {
            string packingLabel = "Packing Label:\n";
            foreach (Product product in products)
            {
                packingLabel += product.GetPackingLabel() + "\n";
            }

            return packingLabel;
        }

        // Returns the shipping label (customer's name and address)
        public string GetShippingLabel()
        {
            return $"Shipping Label:\n{customer.GetCustomerDetails()}";
        }
    }

    // Program class
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create addresses
            Address address1 = new Address("25 Abojupa", "Ibadan", "OY", "Nigeria");
            Address address2 = new Address("6 Bonke", "Lagos", "LG", "Nigeria");

            // Create customers
            Customer customer1 = new Customer("Folusho Sanni", address1);
            Customer customer2 = new Customer("Seun Ojebode", address2);

            // Create products
            Product product1 = new Product("Charger", "M198", 49.99, 1);
            Product product2 = new Product("Phone", "C667", 50.50, 2);
            Product product3 = new Product("Screen", "ZP089", 250.75, 1);
            Product product4 = new Product("Fan", "T983", 66.00, 1);

            // Create first order for customer1
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            // Create second order for customer2
            Order order2 = new Order(customer2);
            order2.AddProduct(product3);
            order2.AddProduct(product4);

            // Display order1 details
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order1.GetTotalCost():0.00}\n");

            // Display order2 details
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total Cost: ${order2.GetTotalCost():0.00}\n");
        }
    }
}
