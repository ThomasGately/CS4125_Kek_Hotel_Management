using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CS4125_Kek_Hotel_Management.Models;
using CS4125_Kek_Hotel_Management.Controllers;

namespace CS4125_Kek_Hotel_Management.Payment
{
    //https://refactoring.guru/design-patterns/decorator/csharp/example#lang-features
    // The base Component interface defines operations that can be altered by
    // decorators.
    public abstract class Payment
    {
        public Booking bookingItem;
        public bool sale;
        public double total;
        public double bookingDiscount = 0;
        public DateTime DateTime = DateTime.Now;
        public abstract string Operation();



    }

    // Concrete Components provide default implementations of the operations.
    // There might be several variations of these classes.
    class ConcreteComponent : Payment
    {

        public override string Operation()  //Base class which will be called by further decorators.
        {
            total = bookingItem.Price;
            total = total - (total / bookingDiscount);
            return "Payment:" + total + " Discount:" + bookingDiscount + "/n";
        }

    }

    // The base Decorator class follows the same interface as the other
    // components. The primary purpose of this class is to define the wrapping
    // interface for all concrete decorators. The default implementation of the
    // wrapping code might include a field for storing a wrapped component and
    // the means to initialize it.
    abstract class Decorator : Payment
    {
        protected Payment _Payment;

        public Decorator(Payment component)
        {
            this._Payment = component;
        }

        public void SetComponent(Payment component)
        {
            this._Payment = component;
        }

        // The Decorator delegates all work to the wrapped component.
        public override string Operation()
        {
            if (this._Payment != null)
            {
                return this._Payment.Operation();
            }
            else
            {
                return string.Empty;
            }
        }
    }

    // Concrete Decorators call the wrapped object and alter its result in some
    // way.
    class LoyaltyDiscount : Decorator
    {
        public LoyaltyDiscount(Payment comp) : base(comp)
        {
        }

        // Decorators may call parent implementation of the operation, instead
        // of calling the wrapped object directly. This approach simplifies
        // extension of decorator classes.
        public override string Operation()
        {
            if (bookingItem.BookedCustomer.LoyalityDiscount == 1)
            { bookingDiscount = bookingDiscount + ((total / 100) * 5); }
            if (bookingItem.BookedCustomer.LoyalityDiscount == 2)
            { bookingDiscount = bookingDiscount + ((total / 100) * 10); }
            if (bookingItem.BookedCustomer.LoyalityDiscount == 3)
            { bookingDiscount = bookingDiscount + ((total / 100) * 15); }


            return $"LoyaltyDiscount({base.Operation()})";
        }
    }

    // Decorators can execute their behavior either before or after the call to
    // a wrapped object.
    class FathersDayDiscount : Decorator
    {
        public FathersDayDiscount(Payment comp) : base(comp)
        {
        }

        public override string Operation()
        {
            if (DateTime.Now.ToString("MM/dd/yyyy") == "06/21/2020")
            { bookingDiscount = bookingDiscount + 20; }
            Console.WriteLine("Fathers Day 20% Discount/n");
            return $"SaleDiscount ({base.Operation()})";
        }
    }

    public class Client
    {
        // The client code works with all objects using the Component interface.
        // This way it can stay independent of the concrete classes of
        // components it works with.
        public void ClientCode(Payment component)
        {
            component.Operation();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            var simple = new ConcreteComponent();

           
            LoyaltyDiscount decorator1 = new LoyaltyDiscount(simple);
            FathersDayDiscount decorator2 = new FathersDayDiscount(decorator1);
          
        }
    }
}