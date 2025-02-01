using System;
using System.Collections.Generic;

public class Address
{
    private string _streetAddress;
    private string _city;
    private string _stateProvince;
    private string _country;

    public Address(string streetAddress, string city, string stateProvince, string country)
    {
        _streetAddress = streetAddress;
        _city = city;
        _stateProvince = stateProvince;
        _country = country;
    }

    public string StreetAddress { get => _streetAddress; set => _streetAddress = value; }
    public string City { get => _city; set => _city = value; }
    public string StateProvince { get => _stateProvince; set => _stateProvince = value; }
    public string Country { get => _country; set => _country = value; }


    public bool IsInUSA()
    {
        return _country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    public string GetFullAddress()
    {
        return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
    }
}

public class Customer
{
    private string _name;
    private Address _address;

    public Customer(string name, Address address)
    {
        _name = name;
        _address = address;
    }

    public string Name { get => _name; set => _name = value; }
    public Address Address { get => _address; set => _address = value; }

    public bool IsInUSA()
    {
        return _address.IsInUSA();
    }
}

public class Product
{
    private string _name;
    private int _productId;
    private double _price;
    private int _quantity;

    public Product(string name, int productId, double price, int quantity)
    {
        _name = name;
        _productId = productId;
        _price = price;
        _quantity = quantity;
    }

    public string Name { get => _name; set => _name = value; }
    public int ProductId { get => _productId; set => _productId = value; }
    public double Price { get => _price; set => _price = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }


    public double GetTotalCost()
    {
        return _price * _quantity;
    }
}

public class Order
{
    private Customer _customer;
    private List<Product> _products;

    public Order(Customer customer)
    {
        _customer = customer;
        _products = new List<Product>();
    }

    public Customer Customer { get => _customer; set => _customer = value; }
    public List<Product> Products { get => _products; set => _products = value; }


    public void AddProduct(Product product)
    {
        _products.Add(product);
    }

    public double CalculateTotalCost()
    {
        double totalProductCost = 0;
        foreach (Product product in _products)
        {
            totalProductCost += product.GetTotalCost();
        }

        double shippingCost = _customer.IsInUSA() ? 5 : 35;
        return totalProductCost + shippingCost;
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (Product product in _products)
        {
            label += $"{product.Name} (ID: {product.ProductId})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{_customer.Name}\n{_customer.Address.GetFullAddress()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "Utah", "Salt Lake", "USA");
        Address address2 = new Address("106 Yisa Jinadu", "Ojo", "Lagos", "Nigeria");
        Address address3 = new Address("789 Oak Ln", "London", "England", "UK");


        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Chinedu K. Amuji", address2);
        Customer customer3 = new Customer("Alice Johnson", address3);


        // Create products
        Product product1 = new Product("Laptop", 1, 1200.00, 1);
        Product product2 = new Product("Mouse", 2, 25.00, 2);
        Product product3 = new Product("Keyboard", 3, 75.00, 1);
        Product product4 = new Product("Monitor", 4, 300, 1);
        Product product5 = new Product("Webcam", 5, 50, 1);


        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);


        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);

        Order order3 = new Order(customer3);
        order3.AddProduct(product2);
        order2.AddProduct(product3);
        order3.AddProduct(product5);

        // Display order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Order 1 Total Cost: ${order1.CalculateTotalCost()}");
        Console.WriteLine();

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Order 2 Total Cost: ${order2.CalculateTotalCost()}");
        Console.WriteLine();

     Console.WriteLine(order3.GetPackingLabel());
        Console.WriteLine(order3.GetShippingLabel());
        Console.WriteLine($"Order 1 Total Cost: ${order3.CalculateTotalCost()}");   


    } 
}