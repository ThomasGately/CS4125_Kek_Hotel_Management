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
        public double price;
        public int discountLevel;
        public abstract double getTotal();
    }

    // Concrete Components provide default implementations of the operations.
    // There might be several variations of these classes.
    class BasePayment : Payment
    {



        public BasePayment(Booking Input)
        {
            this.price = Input.Price;
            this.discountLevel = Input.ApplicationUser.LoyalityDiscount;
        }

        public override double getTotal()//Returns flat rate no discount.
        {

            return this.price;
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
        public override double getTotal()
        {
            if (this._Payment != null)
            {
                return this._Payment.getTotal();
            }
            else
            {
                return -1;
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
        public override double getTotal()
        {
            if (discountLevel == 1)
            { price = price - ((price / 100) * 5); }
            if (discountLevel == 2)
            { price = price - ((price / 100) * 10); }
            if (discountLevel == 3)
            { price = price - ((price / 100) * 15); }


            return base.getTotal();
        }
    }
    /*
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
    */
    public class Client
    {
        // The client code works with all objects using the Component interface.
        // This way it can stay independent of the concrete classes of
        // components it works with.
        public void ClientCode(Payment component)
        {
            component.getTotal();
        }
    }
    /*
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

           // var simple = new BasePayment();

            // ...as well as decorated ones.
            //
            // Note how decorators can wrap not only simple components but the
            // other decorators as well.
            //LoyaltyDiscount decorator1 = new LoyaltyDiscount(simple);
            //FathersDayDiscount decorator2 = new FathersDayDiscount(decorator1);
            //Console.WriteLine("Client: Now I've got a decorated component:");
            //client.ClientCode(decorator2);
        }
    }
    */
}



